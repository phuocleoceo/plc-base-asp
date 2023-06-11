using PlcBase.Features.AccessControl.DTOs;

namespace PlcBase.Features.AccessControl.Services;

public interface IAccessControlService
{
    Task<List<RoleDTO>> GetRoles();
}
