using AutoMapper;

using PlcBase.Features.User.Entities;
using PlcBase.Features.User.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;
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
}
