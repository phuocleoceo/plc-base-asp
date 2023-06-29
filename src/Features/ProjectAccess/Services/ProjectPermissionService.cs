using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;

namespace PlcBase.Features.ProjectAccess.Services;

public class ProjectPermissionService : IProjectPermissionService
{
    private readonly IPermissionHelper _permissionHelper;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectPermissionService(
        IPermissionHelper permissionHelper,
        IUnitOfWork uow,
        IMapper mapper
    )
    {
        _uow = uow;
        _mapper = mapper;
        _permissionHelper = permissionHelper;
    }

    public async Task<IEnumerable<ProjectPermissionGroupDTO>> GetForProjectRole(int projectRoleId)
    {
        List<PermissionContent> allPermissions = _permissionHelper.GetAllPermissions();

        IEnumerable<string> projectPermissions = (
            await _uow.ProjectPermission.GetForProjectRole(projectRoleId)
        ).Select(pm => pm.Key);

        return allPermissions
            .GroupBy(p => p.Key.Split(".")[0])
            .Select(
                p =>
                    new ProjectPermissionGroupDTO()
                    {
                        Module = p.Key,
                        Children = p.Select(
                                m =>
                                    new ProjectPermissionDTO()
                                    {
                                        Key = m.Key,
                                        Description = m.Description,
                                        IsGranted = projectPermissions.Contains(m.Key)
                                    }
                            )
                            .OrderBy(m => m.Key)
                    }
            )
            .OrderBy(p => p.Module);
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
