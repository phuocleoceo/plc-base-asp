using AutoMapper;

using PlcBase.Features.Project.Entities;
using PlcBase.Features.Project.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Project.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<bool> CreateProject(ReqUser reqUser, CreateProjectDTO createProjectDTO)
    {
        ProjectEntity projectEntity = _mapper.Map<ProjectEntity>(createProjectDTO);
        projectEntity.CreatorId = reqUser.Id;
        projectEntity.LeaderId = reqUser.Id;

        _uow.Project.Add(projectEntity);
        return await _uow.Save() > 0;
    }
}
