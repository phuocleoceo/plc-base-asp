using AutoMapper;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

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

    public async Task<int> GetIndexForNewStatus(int projectId)
    {
        ProjectStatusEntity projectStatus = await GetOneAsync<ProjectStatusEntity>(
            new QueryModel<ProjectStatusEntity>()
            {
                OrderBy = c => c.OrderByDescending(s => s.Index),
                Filters = { s => s.ProjectId == projectId }
            }
        );

        return projectStatus?.Index + 1 ?? 0;
    }
}
