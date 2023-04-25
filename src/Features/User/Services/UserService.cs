using AutoMapper;

using PlcBase.Features.User.Entities;
using PlcBase.Features.User.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;
using PlcBase.Shared.Constants;

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
}
