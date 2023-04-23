using AutoMapper;

using PlcBase.Features.Invitation.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Invitation.Repositories;

public class InvitationRepository : BaseRepository<InvitationEntity>, IInvitationRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public InvitationRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
