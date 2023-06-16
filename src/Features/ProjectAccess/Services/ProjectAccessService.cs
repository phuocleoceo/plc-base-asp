using AutoMapper;

using PlcBase.Common.Repositories;

namespace PlcBase.Features.ProjectAccess.Services;

public class ProjectAccessService : IProjectAccessService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectAccessService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
}
