using AutoMapper;

using PlcBase.Features.Project.Entities;
using PlcBase.Features.Project.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

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

    public async Task<bool> UpdateProject(
        ReqUser reqUser,
        int projectId,
        UpdateProjectDTO updateProjectDTO
    )
    {
        ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser, projectId);

        if (projectDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");

        _mapper.Map(updateProjectDTO, projectDb);
        _uow.Project.Update(projectDb);
        return await _uow.Save() > 0;
    }

    public async Task<bool> DeleteProject(ReqUser reqUser, int projectId)
    {
        ProjectEntity projectDb = await _uow.Project.GetByIdAndOwner(reqUser, projectId);

        if (projectDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_not_found");

        _uow.Project.SoftDelete(projectDb);
        return await _uow.Save() > 0;
    }
}
