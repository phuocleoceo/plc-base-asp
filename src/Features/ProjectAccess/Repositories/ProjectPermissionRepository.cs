using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectAccess.Repositories;

public class ProjectPermissionRepository
    : BaseRepository<ProjectPermissionEntity>,
        IProjectPermissionRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProjectPermissionRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
