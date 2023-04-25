using AutoMapper;

using PlcBase.Features.User.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.User.DTOs;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.User.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<PagedList<UserDTO>> GetAllUsers(UserParams userParams)
    {
        return await _uow.UserProfile.GetPagedAsync<UserDTO>(
            new QueryModel<UserProfileEntity>()
            {
                OrderBy = c => c.OrderByDescending(up => up.CreatedAt),
                Includes =
                {
                    up => up.UserAccount,
                    up => up.AddressWard.AddressDistrict.AddressProvince,
                },
                PageSize = userParams.PageSize,
                PageNumber = userParams.PageNumber,
            }
        );
    }

    public async Task<UserProfilePersonalDTO> GetUserProfilePersonal(ReqUser reqUser)
    {
        return await _uow.UserProfile.GetOneAsync<UserProfilePersonalDTO>(
                new QueryModel<UserProfileEntity>()
                {
                    Includes =
                    {
                        up => up.UserAccount,
                        up => up.AddressWard.AddressDistrict.AddressProvince,
                    },
                    Filters = { up => up.UserAccountId == reqUser.Id },
                }
            ) ?? throw new BaseException(HttpCode.NOT_FOUND, "user_not_found");
    }

    public async Task<UserProfileAnonymousDTO> GetUserProfileAnonymous(int userId)
    {
        return await _uow.UserProfile.GetOneAsync<UserProfileAnonymousDTO>(
                new QueryModel<UserProfileEntity>()
                {
                    Includes =
                    {
                        up => up.UserAccount,
                        up => up.AddressWard.AddressDistrict.AddressProvince,
                    },
                    Filters = { up => up.UserAccountId == userId },
                }
            ) ?? throw new BaseException(HttpCode.NOT_FOUND, "user_not_found");
    }

    public async Task<UserAccountDTO> GetUserAccountById(int userId)
    {
        return await _uow.UserAccount.GetOneAsync<UserAccountDTO>(
                new QueryModel<UserAccountEntity>()
                {
                    Includes = { ua => ua.Role, },
                    Filters = { ua => ua.Id == userId },
                }
            ) ?? throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");
    }

    public async Task<bool> UpdateUserProfile(
        ReqUser reqUser,
        UserProfileUpdateDTO userProfileUpdateDTO
    )
    {
        UserProfileEntity userProfileDb = await _uow.UserProfile.GetOneAsync<UserProfileEntity>(
            new QueryModel<UserProfileEntity>()
            {
                Filters = { up => up.UserAccountId == reqUser.Id },
            }
        );
        if (userProfileDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "user_not_found");

        _mapper.Map(userProfileUpdateDTO, userProfileDb);
        _uow.UserProfile.Update(userProfileDb);
        return await _uow.Save() > 0;
    }

    public async Task<bool> UpdateUserAccount(int userId, UserAccountUpdateDTO userAccountUpdateDTO)
    {
        UserAccountEntity userAccountDb = await _uow.UserAccount.GetOneAsync<UserAccountEntity>(
            new QueryModel<UserAccountEntity>() { Filters = { ua => ua.Id == userId }, }
        );
        if (userAccountDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "account_not_found");

        _mapper.Map(userAccountUpdateDTO, userAccountDb);
        _uow.UserAccount.Update(userAccountDb);
        return await _uow.Save() > 0;
    }
}
