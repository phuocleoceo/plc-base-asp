using AutoMapper;

using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
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

    public Task<List<ProjectRoleDTO>> GetAllProjectRoles()
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<ProjectRoleDTO>> GetProjectRoles(ProjectRoleParams roleParams)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectRoleDTO> GetProjectRoleById(int projectRoleId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateProjectRole(CreateProjectRoleDTO createRoleDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProjectRole(int projectRoleId, UpdateProjectRoleDTO updateRoleDTO)
    {
        throw new NotImplementedException();
    }
}
