using AutoMapper;

using PlcBase.Features.Sprint.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Sprint.Repositories;

public class SprintRepository : BaseRepository<SprintEntity>, ISprintRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public SprintRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<SprintEntity> GetForUpdateAndDelete(int projectId, int sprintId)
    {
        return await GetOneAsync<SprintEntity>(
            new QueryModel<SprintEntity>()
            {
                Filters = { i => i.Id == sprintId && i.ProjectId == projectId },
            }
        );
    }

    public async Task<SprintEntity> GetAvailableSprint(int projectId)
    {
        return await GetOneAsync<SprintEntity>(
            new QueryModel<SprintEntity>()
            {
                Filters = { i => i.ProjectId == projectId && i.CompletedAt == null },
            }
        );
    }
}
