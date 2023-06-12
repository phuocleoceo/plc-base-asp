using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

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

    public async Task<SprintDTO> GetAvailableSprint(int projectId)
    {
        return await _uow.Sprint.GetOneAsync<SprintDTO>(
            new QueryModel<SprintEntity>()
            {
                Filters = { i => i.ProjectId == projectId && i.IsInProgress == true },
            }
        );
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

    public async Task<bool> UpdateSprint(
        ReqUser reqUser,
        int projectId,
        int sprintId,
        UpdateSprintDTO updateSprintDTO
    )
    {
        SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

        if (sprintDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

        _mapper.Map(updateSprintDTO, sprintDb);
        _uow.Sprint.Update(sprintDb);
        return await _uow.Save();
    }

    public async Task<bool> DeleteSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

        if (sprintDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

        _uow.Sprint.Remove(sprintDb);
        return await _uow.Save();
    }

    public async Task<bool> StartSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

        if (sprintDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

        sprintDb.IsInProgress = true;
        _uow.Sprint.Update(sprintDb);
        return await _uow.Save();
    }

    public async Task<bool> CompleteSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

        if (sprintDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

        sprintDb.IsInProgress = false;
        _uow.Sprint.Update(sprintDb);
        return await _uow.Save();
    }
}
