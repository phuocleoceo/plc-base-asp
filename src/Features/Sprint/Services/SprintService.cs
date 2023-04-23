using AutoMapper;

using PlcBase.Common.Repositories;

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
}
