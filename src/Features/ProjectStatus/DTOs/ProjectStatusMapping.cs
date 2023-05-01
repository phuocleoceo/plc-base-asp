using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;

namespace PlcBase.Features.ProjectStatus.DTOs;

public class ProjectStatusMapping : Profile
{
    public ProjectStatusMapping()
    {
        CreateMap<CreateProjectStatusDTO, ProjectStatusEntity>();
    }
}
