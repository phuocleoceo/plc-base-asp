using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectAccess.Repositories;

public class ProjectRoleRepository : BaseRepository<ProjectRoleEntity>, IProjectRoleRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProjectRoleRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
