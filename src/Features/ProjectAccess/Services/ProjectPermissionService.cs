using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.ProjectAccess.Services;

public class ProjectPermissionService : IProjectPermissionService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectPermissionService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<ProjectPermissionDTO>> GetForProjectRole(int projectRoleId)
    {
        return await _uow.ProjectPermission.GetManyAsync<ProjectPermissionDTO>(
            new QueryModel<ProjectPermissionEntity>()
            {
                Filters = { pm => pm.ProjectRoleId == projectRoleId }
            }
        );
    }

    public Task<bool> CreateProjectPermission(
        int projectRoleId,
        CreateProjectPermissionDTO createProjectPermissionDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProjectPermission(int projectRoleId, int projectPermissionId)
    {
        throw new NotImplementedException();
    }
}
