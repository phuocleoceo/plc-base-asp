using System.Collections.Specialized;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Web;
using AutoMapper;

using PlcBase.Features.User.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.Auth.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;

namespace PlcBase.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly IJwtHelper _jwtHelper;
    private readonly IMapper _mapper;
    private readonly ClientAppSettings _clientAppSettings;
    private readonly ISendMailHelper _sendMailHelper;

    public AuthService(
        IUnitOfWork uow,
        IJwtHelper jwtHelper,
        IMapper mapper,
        IOptions<ClientAppSettings> clientAppSettings,
        ISendMailHelper sendMailHelper
    )
    {
        _uow = uow;
        _jwtHelper = jwtHelper;
        _mapper = mapper;
        _clientAppSettings = clientAppSettings.Value;
        _sendMailHelper = sendMailHelper;
    }

    #region Login

    public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
    {
        UserAccountEntity currentUser = await _uow.UserAccount.GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>()
            {
                Filters = { u => u.Email == userLoginDTO.Email },
                Includes = { u => u.Role },
            }
        );

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_not_verified");

        if (!currentUser.IsActived)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_inactived");

        if (!PasswordUtility.IsValidPassword(userLoginDTO.Password, currentUser.Password))
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_password");

        List<Claim> userClaims = await GetUserClaims(currentUser);
        TokenData accessToken = _jwtHelper.CreateToken(userClaims);
        TokenData refreshToken = _jwtHelper.CreateRefreshToken();

        currentUser.RefreshToken = refreshToken.Token;
        currentUser.RefreshTokenExpiredAt = refreshToken.ExpiredAt;
        _uow.UserAccount.Update(currentUser);
        await _uow.Save();

        return new UserLoginResponseDTO()
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            RoleId = currentUser.RoleId,
            RoleName = currentUser.Role.Name,
            AccessToken = accessToken.Token,
            AccessTokenExpiredAt = accessToken.ExpiredAt,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiredAt = refreshToken.ExpiredAt,
        };
    }

    private async Task<List<Claim>> GetUserClaims(UserAccountEntity user)
    {
        // TODO: Permissions or Role
        List<Claim> claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id.ToString()),
            new(CustomClaimTypes.Email, user.Email),
            new(CustomClaimTypes.Role, user.Role.Name)
        };

        return await Task.FromResult(claims);
    }

    public async Task<UserRefreshTokenResponseDTO> RefreshToken(
        UserRefreshTokenDTO userRefreshTokenDTO
    )
    {
        ClaimsPrincipal principal = _jwtHelper.GetPrincipalFromExpiredToken(
            userRefreshTokenDTO.AccessToken
        );

        int userId = Convert.ToInt32(principal.FindFirstValue(CustomClaimTypes.UserId));
        UserAccountEntity currentUser = await _uow.UserAccount.FindByIdAsync(userId);

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_has_token_not_found");

        if (
            currentUser.RefreshToken != userRefreshTokenDTO.RefreshToken
            || currentUser.RefreshTokenExpiredAt <= DateTime.UtcNow
        )
            throw new BaseException(HttpCode.BAD_REQUEST, "refresh_token_invalid_or_expired");

        TokenData accessToken = _jwtHelper.CreateToken(principal.Claims);
        TokenData refreshToken = _jwtHelper.CreateRefreshToken();

        currentUser.RefreshToken = refreshToken.Token;
        _uow.UserAccount.Update(currentUser);
        await _uow.Save();

        return new UserRefreshTokenResponseDTO()
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiredAt = accessToken.ExpiredAt,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiredAt = currentUser.RefreshTokenExpiredAt.Value,
        };
    }

    public async Task<bool> RevokeRefreshToken(ReqUser reqUser)
    {
        UserAccountEntity currentUser = await _uow.UserAccount.FindByIdAsync(reqUser.Id);

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        currentUser.RefreshToken = null;
        currentUser.RefreshTokenExpiredAt = null;

        _uow.UserAccount.Update(currentUser);
        return await _uow.Save();
    }

    #endregion

    #region Register

    public async Task<UserRegisterResponseDTO> Register(UserRegisterDTO userRegisterDTO)
    {
        try
        {
            await _uow.CreateTransaction();

            UserAccountEntity newUserAccount = _mapper.Map<UserAccountEntity>(userRegisterDTO);
            UserProfileEntity newUserProfile = _mapper.Map<UserProfileEntity>(userRegisterDTO);

            // Throw exception if register unique info is exists
            if (await _uow.UserAccount.AnyAsync(ua => ua.Email == newUserAccount.Email))
                throw new BaseException(HttpCode.BAD_REQUEST, "email_existed");

            if (
                await _uow.UserProfile.AnyAsync(
                    up =>
                        up.PhoneNumber == newUserProfile.PhoneNumber
                        || up.IdentityNumber == newUserProfile.IdentityNumber
                )
            )
                throw new BaseException(
                    HttpCode.BAD_REQUEST,
                    "phone_number_or_identity_number_existed"
                );

            // Create user account
            newUserAccount.Password = PasswordUtility.GetPasswordHash(userRegisterDTO.Password);
            newUserAccount.IsVerified = false;
            newUserAccount.IsActived = true;
            newUserAccount.SecurityCode = CodeSecure.CreateRandomCode();

            _uow.UserAccount.Add(newUserAccount);
            await _uow.Save();

            // Create user profile with new user account Id
            newUserProfile.CurrentCredit = 0;
            newUserProfile.UserAccountId = newUserAccount.Id;
            _uow.UserProfile.Add(newUserProfile);

            // await SendMailConfirm(newUserAccount);

            await _uow.Save();
            await _uow.CommitTransaction();

            return new UserRegisterResponseDTO()
            {
                Id = newUserAccount.Id,
                Email = newUserAccount.Email,
            };
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    private async Task SendMailConfirm(UserAccountEntity newUserAccount)
    {
        string webClientPath =
            $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.ConfirmEmailPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = newUserAccount.Id.ToString();
        query["code"] = newUserAccount.SecurityCode;
        uriBuilder.Query = query.ToString() ?? string.Empty;

        await _sendMailHelper.SendEmailAsync(
            new MailContent()
            {
                ToEmail = newUserAccount.Email,
                Subject = "Confirm email to use PLC Base",
                Body =
                    $"Confirm the registration by clicking on the <a href='{uriBuilder}'>link</a>"
            }
        );
    }

    public async Task<bool> ConfirmEmail(UserConfirmEmailDTO userConfirmEmailDTO)
    {
        UserAccountEntity currentUser = await _uow.UserAccount.FindByIdAsync(
            userConfirmEmailDTO.UserId
        );

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_already_verified");

        if (currentUser.SecurityCode != userConfirmEmailDTO.Code)
            throw new BaseException(HttpCode.BAD_REQUEST, "security_code_invalid");

        currentUser.IsVerified = true;
        currentUser.SecurityCode = "";

        _uow.UserAccount.Update(currentUser);
        return await _uow.Save();
    }

    #endregion

    #region Other Auth

    public async Task<bool> ChangePassword(
        ReqUser reqUser,
        UserChangePasswordDTO userChangePasswordDTO
    )
    {
        UserAccountEntity currentUser = await _uow.UserAccount.FindByIdAsync(reqUser.Id);

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_unverified");

        if (
            !PasswordUtility.IsValidPassword(
                userChangePasswordDTO.OldPassword,
                currentUser.Password
            )
        )
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_old_password");

        currentUser.Password = PasswordUtility.GetPasswordHash(userChangePasswordDTO.NewPassword);

        _uow.UserAccount.Update(currentUser);
        return await _uow.Save();
    }

    public async Task ForgotPassword(UserForgotPasswordDTO userForgotPasswordDTO)
    {
        string identityInformation = userForgotPasswordDTO.IdentityInformation;

        UserAccountEntity currentUser = await _uow.UserAccount.GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>()
            {
                Includes = { ua => ua.UserProfile },
                Filters =
                {
                    ua =>
                        ua.Email == identityInformation
                        || ua.UserProfile.PhoneNumber == identityInformation
                        || ua.UserProfile.IdentityNumber == identityInformation
                }
            }
        );

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_unverified");

        string newSecurityCode = CodeSecure.CreateRandomCode();
        currentUser.SecurityCode = newSecurityCode;

        _uow.UserAccount.Update(currentUser);
        await _uow.Save();

        await SendMailForgotPassword(currentUser);
    }

    private async Task SendMailForgotPassword(UserAccountEntity currentUserAccount)
    {
        string webClientPath =
            $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.RecoverPasswordPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = currentUserAccount.Id.ToString();
        query["code"] = currentUserAccount.SecurityCode;
        uriBuilder.Query = query.ToString();

        await _sendMailHelper.SendEmailAsync(
            new MailContent()
            {
                ToEmail = currentUserAccount.Email,
                Subject = "Password recovery for your PLC Base account",
                Body = $"Clicking on the <a href='{uriBuilder}'>link</a> to recover your password"
            }
        );
    }

    public async Task<bool> RecoverPassword(UserRecoverPasswordDTO userRecoverPasswordDTO)
    {
        UserAccountEntity currentUser = await _uow.UserAccount.FindByIdAsync(
            userRecoverPasswordDTO.UserId
        );

        if (currentUser == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        if (!currentUser.IsVerified)
            throw new BaseException(HttpCode.BAD_REQUEST, "account_unverified");

        if (currentUser.SecurityCode != userRecoverPasswordDTO.Code)
            throw new BaseException(HttpCode.BAD_REQUEST, "security_code_invalid");

        currentUser.Password = PasswordUtility.GetPasswordHash(userRecoverPasswordDTO.NewPassword);
        currentUser.SecurityCode = "";

        _uow.UserAccount.Update(currentUser);
        return await _uow.Save();
    }

    #endregion
}
