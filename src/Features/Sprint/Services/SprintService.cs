using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Issue.Entities;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Utilities;
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

    public async Task<SprintDTO> GetSprintById(int projectId, int sprintId)
    {
        return await _uow.Sprint.GetOneAsync<SprintDTO>(
            new QueryModel<SprintEntity>()
            {
                Filters = { i => i.Id == sprintId && i.ProjectId == projectId },
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
        try
        {
            await _uow.CreateTransaction();

            SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

            if (sprintDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

            _uow.Sprint.Remove(sprintDb);

            await _uow.Issue.MoveIssueFromSprintToBacklog(sprintId, projectId);

            await _uow.Save();
            await _uow.CommitTransaction();
            return true;
        }
        catch (BaseException ex)
        {
            await _uow.AbortTransaction();
            throw ex;
        }
    }

    public async Task<bool> StartSprint(ReqUser reqUser, int projectId, int sprintId)
    {
        SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

        if (sprintDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

        sprintDb.StartedAt = TimeUtility.Now();
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
            await _uow.CreateTransaction();

            if (
                completeSprintDTO.MoveType != "backlog"
                && completeSprintDTO.MoveType != "next_sprint"
            )
                throw new BaseException(HttpCode.BAD_REQUEST, "invalid_move_type");

            SprintEntity sprintDb = await _uow.Sprint.GetForUpdateAndDelete(projectId, sprintId);

            if (sprintDb == null)
                throw new BaseException(HttpCode.NOT_FOUND, "sprint_not_found");

            sprintDb.CompletedAt = TimeUtility.Now();
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

            if (completeSprintDTO.MoveType == "backlog")
            {
                await _uow.Issue.MoveIssueToBacklog(completeSprintDTO.UnCompletedIssues, projectId);
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

                await _uow.Issue.MoveIssueToSprint(
                    completeSprintDTO.UnCompletedIssues,
                    nextSprint.Id
                );
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
