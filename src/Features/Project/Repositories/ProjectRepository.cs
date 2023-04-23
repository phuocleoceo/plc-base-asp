using AutoMapper;

using PlcBase.Features.Project.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Project.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public ProjectRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
