using PlcBase.Base.DomainModel;
using PlcBase.Features.Sprint.DTOs;

namespace PlcBase.Features.Sprint.Services;

public interface ISprintService
{
    Task<SprintDTO> GetAvailableSprint(int projectId);

    Task<SprintDTO> GetSprintById(int projectId, int sprintId);

    Task<bool> CreateSprint(ReqUser reqUser, int projectId, CreateSprintDTO createSprintDTO);

    Task<bool> UpdateSprint(
        ReqUser reqUser,
        int projectId,
        int sprintId,
        UpdateSprintDTO updateSprintDTO
    );

    Task<bool> DeleteSprint(ReqUser reqUser, int projectId, int sprintId);

    Task<bool> StartSprint(ReqUser reqUser, int projectId, int sprintId);

    Task<bool> CompleteSprint(
        ReqUser reqUser,
        int projectId,
        int sprintId,
        CompleteSprintDTO completeSprintDTO
    );
}
