using AutoMapper;

using PlcBase.Features.Project.Entities;

namespace PlcBase.Features.Project.DTOs;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<CreateProjectDTO, ProjectEntity>();

        CreateMap<UpdateProjectDTO, ProjectEntity>();
    }
}
