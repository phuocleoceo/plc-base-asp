using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Services;

public class ProjectRoleService : IProjectRoleService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectRoleService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<ProjectRoleDTO>> GetAllProjectRoles()
    {
        return await _uow.ProjectRole.GetManyAsync<ProjectRoleDTO>(
            new QueryModel<ProjectRoleEntity>() { OrderBy = c => c.OrderBy(r => r.CreatedAt) }
        );
    }

    public async Task<PagedList<ProjectRoleDTO>> GetProjectRoles(ProjectRoleParams roleParams)
    {
        QueryModel<ProjectRoleEntity> roleQuery = new QueryModel<ProjectRoleEntity>()
        {
            OrderBy = c => c.OrderBy(r => r.CreatedAt),
            PageSize = roleParams.PageSize,
            PageNumber = roleParams.PageNumber
        };

        if (!string.IsNullOrWhiteSpace(roleParams.SearchValue))
        {
            string searchValue = roleParams.SearchValue.ToLower();
            roleQuery.Filters.Add(
                r =>
                    r.Name.ToLower().Contains(searchValue)
                    || r.Description.ToLower().Contains(searchValue)
            );
        }

        return await _uow.ProjectRole.GetPagedAsync<ProjectRoleDTO>(roleQuery);
    }

    public async Task<ProjectRoleDTO> GetProjectRoleById(int projectRoleId)
    {
        return await _uow.ProjectRole.GetOneAsync<ProjectRoleDTO>(
                new QueryModel<ProjectRoleEntity>() { Filters = { r => r.Id == projectRoleId } }
            ) ?? throw new BaseException(HttpCode.NOT_FOUND, "project_role_not_found");
    }

    public async Task<bool> CreateProjectRole(CreateProjectRoleDTO createRoleDTO)
    {
        ProjectRoleEntity projectRoleEntity = _mapper.Map<ProjectRoleEntity>(createRoleDTO);

        _uow.ProjectRole.Add(projectRoleEntity);
        return await _uow.Save();
    }

    public async Task<bool> UpdateProjectRole(int projectRoleId, UpdateProjectRoleDTO updateRoleDTO)
    {
        ProjectRoleEntity projectRoleDb = await _uow.ProjectRole.FindByIdAsync(projectRoleId);

        if (projectRoleDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_role_not_found");

        _mapper.Map(updateRoleDTO, projectRoleDb);
        _uow.ProjectRole.Update(projectRoleDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteProjectRole(int projectRoleId)
    {
        ProjectRoleEntity projectRoleDb = await _uow.ProjectRole.FindByIdAsync(projectRoleId);

        if (projectRoleDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_role_not_found");

        _uow.ProjectRole.Remove(projectRoleDb);
        return await _uow.Save();
    }
}
