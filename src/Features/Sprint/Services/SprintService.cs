using AutoMapper;
using PlcBase.Base.DomainModel;
using PlcBase.Common.Repositories;
using PlcBase.Features.Sprint.DTOs;

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

    public Task<bool> CreateSprint(ReqUser reqUser, int projectId, CreateSprintDTO createSprintDTO)
    {
        throw new NotImplementedException();
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
