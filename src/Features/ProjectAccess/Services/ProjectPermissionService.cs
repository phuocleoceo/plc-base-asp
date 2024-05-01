using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Helpers;
using PlcBase.Base.Error;

namespace PlcBase.Features.ProjectAccess.Services;

public class ProjectPermissionService : IProjectPermissionService
{
    private readonly IPermissionHelper _permissionHelper;
    private readonly IRedisHelper _redisHelper;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectPermissionService(
        IPermissionHelper permissionHelper,
        IRedisHelper redisHelper,
        IUnitOfWork uow,
        IMapper mapper
    )
    {
        _uow = uow;
        _mapper = mapper;
        _redisHelper = redisHelper;
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
        await _redisHelper.RemoveMapCache(
            GetPermissionKeysOfRoleRedisKey(),
            projectRoleId.ToString()
        );
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
        await _redisHelper.RemoveMapCache(
            GetPermissionKeysOfRoleRedisKey(),
            projectRoleId.ToString()
        );
        return await _uow.Save();
    }

    public async Task<IEnumerable<string>> GetPermissionKeysOfRole(List<int> projectRoleIds)
    {
        if (projectRoleIds.Count == 0)
        {
            return Enumerable.Empty<string>();
        }

        Dictionary<string, List<string>> permissionsOfRole = await _redisHelper.GetMapCache<
            List<string>
        >(
            GetPermissionKeysOfRoleRedisKey(),
            projectRoleIds.Select(projectRoleId => projectRoleId.ToString()).ToHashSet()
        );

        List<string> permissionKeysInCache = new List<string>();
        List<int> projectRoleIdsNeedCachePermission = new List<int>();

        foreach (int projectRoleId in projectRoleIds)
        {
            permissionsOfRole.TryGetValue(projectRoleId.ToString(), out List<string> permissionKey);
            if (permissionKey == null)
            {
                projectRoleIdsNeedCachePermission.Add(projectRoleId);
                continue;
            }
            permissionKeysInCache.AddRange(permissionKey);
        }

        if (projectRoleIdsNeedCachePermission.Count == 0)
        {
            return permissionKeysInCache;
        }

        List<ProjectPermissionEntity> projectPermissions =
            await _uow.ProjectPermission.GetForProjectRoles(projectRoleIdsNeedCachePermission);

        Dictionary<string, List<string>> permissionsNeedCache = projectPermissions
            .GroupBy(pm => pm.ProjectRoleId.ToString())
            .ToDictionary(group => group.Key, group => group.Select(pm => pm.Key).ToList());

        permissionKeysInCache.AddRange(
            projectPermissions.Select(projectPermission => projectPermission.Key)
        );

        await _redisHelper.SetMapCache(GetPermissionKeysOfRoleRedisKey(), permissionsNeedCache);

        return permissionKeysInCache;
    }

    private string GetPermissionKeysOfRoleRedisKey()
    {
        return RedisUtility.GetKey<ProjectRoleDTO>("permissions");
    }
}
