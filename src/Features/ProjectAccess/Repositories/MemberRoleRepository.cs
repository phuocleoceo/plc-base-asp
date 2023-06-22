using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectAccess.Repositories;

public class MemberRoleRepository : BaseRepository<MemberRoleEntity>, IMemberRoleRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public MemberRoleRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
