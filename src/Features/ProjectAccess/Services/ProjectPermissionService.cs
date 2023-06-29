using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

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

    public async Task<bool> CreateProjectPermission(
        int projectRoleId,
        CreateProjectPermissionDTO createProjectPermissionDTO
    )
    {
        ProjectPermissionEntity projectPermissionEntity = _mapper.Map<ProjectPermissionEntity>(
            createProjectPermissionDTO
        );
        projectPermissionEntity.ProjectRoleId = projectRoleId;

        _uow.ProjectPermission.Add(projectPermissionEntity);
        return await _uow.Save();
    }

    public async Task<bool> DeleteProjectPermission(int projectRoleId, string projectPermissionKey)
    {
        ProjectPermissionEntity projectPermissionDb =
            await _uow.ProjectPermission.GetOneAsync<ProjectPermissionEntity>(
                new QueryModel<ProjectPermissionEntity>()
                {
                    Filters =
                    {
                        pm => pm.Key == projectPermissionKey && pm.ProjectRoleId == projectRoleId
                    }
                }
            );

        if (projectPermissionDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_permission_not_found");

        _uow.ProjectPermission.Remove(projectPermissionDb);
        return await _uow.Save();
    }
}
