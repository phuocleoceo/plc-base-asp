using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.ProjectStatus.Repositories;

public class ProjectStatusRepository : BaseRepository<ProjectStatusEntity>, IProjectStatusRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProjectStatusRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
