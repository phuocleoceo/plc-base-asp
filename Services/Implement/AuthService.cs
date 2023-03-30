using PlcBase.Repositories.Interface;
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

namespace PlcBase.Services.Implement;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uof;
    private readonly IJwtHelper _jwtHelper;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork uof,
                       IJwtHelper jwtHelper,
                       IMapper mapper)
    {
        _uof = uof;
        _jwtHelper = jwtHelper;
        _mapper = mapper;
    }

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
}