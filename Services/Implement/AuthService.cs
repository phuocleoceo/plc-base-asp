using PlcBase.Repositories.Interface;
using System.Collections.Specialized;
using Microsoft.Extensions.Options;
using PlcBase.Services.Interface;
using PlcBase.Base.DomainModel;
using PlcBase.Helpers.Password;
using PlcBase.Common.Constants;
using PlcBase.Models.Entities;
using System.Security.Claims;
using PlcBase.Models.DTO;
using PlcBase.Base.Error;
using PlcBase.Helpers;
using AutoMapper;
using System.Web;

namespace PlcBase.Services.Implement;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uof;
    private readonly IJwtHelper _jwtHelper;
    private readonly IMapper _mapper;
    private readonly ClientAppSettings _clientAppSettings;
    private readonly ISendMailHelper _sendMailHelper;

    public AuthService(IUnitOfWork uof,
                       IJwtHelper jwtHelper,
                       IMapper mapper,
                       IOptions<ClientAppSettings> clientAppSettings,
                       ISendMailHelper sendMailHelper)
    {
        _uof = uof;
        _jwtHelper = jwtHelper;
        _mapper = mapper;
        _clientAppSettings = clientAppSettings.Value;
        _sendMailHelper = sendMailHelper;
    }

    #region Login

    public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
    {
        UserAccountEntity currentUser = await _uof.UserAccount.GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>()
            {
                Filters = { u => u.Email == userLoginDTO.Email },
            }
        );

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_not_verified");

        if (!currentUser.IsActived)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_inactived");

        PasswordHash passwordHash = new PasswordHash(currentUser.PasswordHashed, currentUser.PasswordSalt);
        if (!PasswordSecure.IsValidPasswod(userLoginDTO.Password, passwordHash))
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_password");

        List<Claim> userClaims = await GetUserClaims(currentUser);
        TokenData userToken = _jwtHelper.CreateToken(userClaims);

        return new UserLoginResponseDTO()
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            RoleId = currentUser.RoleId,
            AccessToken = userToken.Token,
            TokenExpiredAt = userToken.ExpiredAt,
        };
    }

    private async Task<List<Claim>> GetUserClaims(UserAccountEntity user)
    {
        // TODO: Permissions

        List<Claim> claims = new List<Claim>();

        claims.Add(new Claim(CustomClaimTypes.UserId, user.Id.ToString()));
        claims.Add(new Claim(CustomClaimTypes.Email, user.Email));

        return await Task.FromResult(claims);
    }

    #endregion

    #region Register

    public async Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO)
    {
        try
        {
            await _uof.CreateTransaction();

            UserAccountEntity newUserAccount = _mapper.Map<UserAccountEntity>(userRegisterDTO);
            UserProfileEntity newUserProfile = _mapper.Map<UserProfileEntity>(userRegisterDTO);

            // Throw exception if register unique info is exists
            if (await _uof.UserAccount.AnyAsync(ua => ua.Email == newUserAccount.Email))
                throw new BaseException(HttpCode.BAD_REQUEST, "email_existed");

            if (await _uof.UserProfile.AnyAsync(up =>
                                    up.PhoneNumber == newUserProfile.PhoneNumber ||
                                    up.IdentityNumber == newUserProfile.IdentityNumber))
                throw new BaseException(HttpCode.BAD_REQUEST, "phone_number_or_identity_number_existed");

            // Create user account
            PasswordHash passwordHash = PasswordSecure.GetPasswordHash(userRegisterDTO.Password);
            newUserAccount.PasswordSalt = passwordHash.PasswordSalt;
            newUserAccount.PasswordHashed = passwordHash.PasswordHashed;
            newUserAccount.IsVerified = false;
            newUserAccount.IsActived = true;
            newUserAccount.SecurityCode = CodeSecure.CreateRandomCode();

            _uof.UserAccount.Add(newUserAccount);
            await _uof.Save();

            // Create user profile with new user account Id
            newUserProfile.CurrentCredit = 0;
            newUserProfile.UserAccountId = newUserAccount.Id;
            _uof.UserProfile.Add(newUserProfile);

            // Send mail
            await SendMailConfirm(newUserAccount);

            await _uof.Save();
            await _uof.CommitTransaction();

            return new UserRegisterResponseDTO()
            {
                Id = newUserAccount.Id,
                Email = newUserAccount.Email,
            };
        }
        catch (BaseException ex)
        {
            await _uof.AbortTransaction();
            throw ex;
        }
    }

    private async Task SendMailConfirm(UserAccountEntity newUserAccount)
    {
        string webClientPath = $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.ConfirmEmailPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = newUserAccount.Id.ToString();
        query["code"] = newUserAccount.SecurityCode;
        uriBuilder.Query = query.ToString();

        await _sendMailHelper.SendEmailAsync(new MailContent()
        {
            ToEmail = newUserAccount.Email,
            Subject = "Confirm email to use PLC Base",
            Body = $"Confirm the registration by clicking on the <a href='{uriBuilder}'>link</a>"
        });
    }

    public async Task<bool> ConfirmEmail(UserConfirmEmailDTO userConfirmEmailDTO)
    {
        UserAccountEntity currentUser = await _uof.UserAccount.FindByIdAsync(userConfirmEmailDTO.UserId);

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_already_verified");

        if (currentUser.SecurityCode != userConfirmEmailDTO.Code)
            throw new BaseException(HttpCode.BAD_REQUEST, "security_code_invalid");

        currentUser.IsVerified = true;
        currentUser.SecurityCode = "";

        _uof.UserAccount.Update(currentUser);
        return await _uof.Save() > 0;
    }

    #endregion

    #region Other Auth

    public async Task<bool> ChangePassword(ReqUser reqUser, UserChangePasswordDTO userChangePasswordDTO)
    {
        UserAccountEntity currentUser = await _uof.UserAccount.FindByIdAsync(reqUser.Id);

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_unverified");

        PasswordHash passwordHashDB = new PasswordHash(currentUser.PasswordHashed, currentUser.PasswordSalt);
        if (!PasswordSecure.IsValidPasswod(userChangePasswordDTO.OldPassword, passwordHashDB))
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_old_password");

        PasswordHash newPasswordHash = PasswordSecure.GetPasswordHash(userChangePasswordDTO.NewPassword);
        currentUser.PasswordHashed = newPasswordHash.PasswordHashed;
        currentUser.PasswordSalt = newPasswordHash.PasswordSalt;

        _uof.UserAccount.Update(currentUser);
        return await _uof.Save() > 0;
    }

    public async Task ForgotPassword(UserForgotPasswordDTO userForgotPasswordDTO)
    {
        string identityInformation = userForgotPasswordDTO.IdentityInformation;

        UserAccountEntity currentUser = await _uof.UserAccount.GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>()
            {
                Includes = { ua => ua.UserProfile },
                Filters = { ua => ua.Email == identityInformation ||
                                  ua.UserProfile.PhoneNumber == identityInformation ||
                                  ua.UserProfile.IdentityNumber == identityInformation }
            });

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_unverified");

        string newSecurityCode = CodeSecure.CreateRandomCode();
        currentUser.SecurityCode = newSecurityCode;

        _uof.UserAccount.Update(currentUser);
        await _uof.Save();

        await SendMailForgotPassword(currentUser);
    }

    private async Task SendMailForgotPassword(UserAccountEntity currentUserAccount)
    {
        string webClientPath = $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.RecoverPasswordPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = currentUserAccount.Id.ToString();
        query["code"] = currentUserAccount.SecurityCode;
        uriBuilder.Query = query.ToString();

        await _sendMailHelper.SendEmailAsync(new MailContent()
        {
            ToEmail = currentUserAccount.Email,
            Subject = "Password recovery for your PLC Base account",
            Body = $"Clicking on the <a href='{uriBuilder}'>link</a> to recover your password"
        });
    }

    #endregion
}