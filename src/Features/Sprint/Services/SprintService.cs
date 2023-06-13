using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Issue.Entities;
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
                Filters = { i => i.ProjectId == projectId && i.CompletedAt == null },
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

        sprintDb.StartedAt = DateTime.UtcNow;
        _uow.Sprint.Update(sprintDb);
        return await _uow.Save();
    }

    public async Task<bool> CompleteSprint(
        ReqUser reqUser,
        int projectId,
        int sprintId,
        CompleteSprintDTO completeSprintDTO
    )
    {
        try
        {
            if (
                completeSprintDTO.MoveType != "backlog"
                && completeSprintDTO.MoveType != "next_sprint"
            )
                throw new BaseException(HttpCode.BAD_REQUEST, "invalid_move_type");

            SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

            if (sprintDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

            await _uow.CreateTransaction();

            sprintDb.CompletedAt = DateTime.UtcNow;
            _uow.Sprint.Update(sprintDb);
            await _uow.Save();

            List<IssueEntity> completedIssues = await _uow.Issue.GetByIds(
                completeSprintDTO.CompletedIssues
            );

            completedIssues.ForEach(i =>
            {
                i.SprintId = null;
                i.BacklogIndex = null;
                _uow.Issue.Update(i);
            });
            await _uow.Save();

            List<IssueEntity> unCompletedIssues = await _uow.Issue.GetByIds(
                completeSprintDTO.UnCompletedIssues
            );

            if (completeSprintDTO.MoveType == "backlog")
            {
                double backlogIndex = _uow.Issue.GetBacklogIndexForNewIssue(projectId);
                unCompletedIssues.ForEach(i =>
                {
                    i.SprintId = null;
                    i.BacklogIndex = backlogIndex++;
                    _uow.Issue.Update(i);
                });
                await _uow.Save();
            }

            if (completeSprintDTO.MoveType == "next_sprint")
            {
                SprintEntity nextSprint = new SprintEntity()
                {
                    Title = "next_sprint",
                    Goal = "",
                    ProjectId = projectId
                };

                _uow.Sprint.Add(nextSprint);
                await _uow.Save();

                unCompletedIssues.ForEach(i =>
                {
                    i.SprintId = nextSprint.Id;
                    i.BacklogIndex = null;
                    _uow.Issue.Update(i);
                });
                await _uow.Save();
            }

            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }
}
