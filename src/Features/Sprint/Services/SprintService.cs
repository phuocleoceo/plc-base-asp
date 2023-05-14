using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Sprint.Services;

public class SprintService : ISprintService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SprintService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<bool> CreateSprint(
        ReqUser reqUser,
        int projectId,
        CreateSprintDTO createSprintDTO
    )
    {
        SprintEntity sprintEntity = _mapper.Map<SprintEntity>(createSprintDTO);

        sprintEntity.ProjectId = projectId;
        sprintEntity.IsInProgress = false;

        _uow.Sprint.Add(sprintEntity);
        return await _uow.Save();
    }

    public Task<bool> UpdateSprint(
        ReqUser reqUser,
        int projectId,
        int sprintId,
        UpdateSprintDTO updateSprintDTO
    )
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> StartSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CompleteSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        throw new NotImplementedException();
    }
}
