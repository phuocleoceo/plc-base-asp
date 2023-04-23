using AutoMapper;

using PlcBase.Common.Repositories;

namespace PlcBase.Features.Invitation.Services;

public class InvitationService : IInvitationService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InvitationService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
}
