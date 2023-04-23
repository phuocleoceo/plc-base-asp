using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectMember.Repositories;

public class ProjectMemberRepository : BaseRepository<ProjectMemberEntity>, IProjectMemberRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProjectMemberRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}