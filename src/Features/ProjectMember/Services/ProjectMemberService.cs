using AutoMapper;

using PlcBase.Common.Repositories;

namespace PlcBase.Features.ProjectMember.Services;

public class ProjectMemberService : IProjectMemberService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectMemberService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
}
