using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Common.Repositories;

namespace PlcBase.Features.AccessControl.Services;

public class AccessControlService : IAccessControlService
{
    private readonly IUnitOfWork _uow;

    public AccessControlService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<RoleDTO>> GetRoles()
    {
        return await _uow.Role.GetManyAsync<RoleDTO>();
    }
}
