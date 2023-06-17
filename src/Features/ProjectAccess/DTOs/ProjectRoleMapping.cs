using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Features.ProjectAccess.DTOs;

public class ProjectRoleMapping : Profile
{
    public ProjectRoleMapping()
    {
        CreateMap<ProjectRoleEntity, ProjectRoleDTO>();

        CreateMap<CreateProjectRoleDTO, ProjectRoleEntity>();

        CreateMap<UpdateProjectRoleDTO, ProjectRoleEntity>();
    }
}
