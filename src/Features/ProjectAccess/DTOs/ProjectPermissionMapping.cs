using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;

namespace PlcBase.Features.ProjectAccess.DTOs;

public class ProjectPermissionMapping : Profile
{
    public ProjectPermissionMapping()
    {
        CreateMap<ProjectPermissionEntity, ProjectPermissionDTO>();

        CreateMap<CreateProjectPermissionDTO, ProjectPermissionEntity>();
    }
}
