using AutoMapper;

using PlcBase.Features.Project.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

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

    public async Task<ProjectEntity> GetByIdAndOwner(ReqUser reqUser, int projectId)
    {
        return await GetOneAsync<ProjectEntity>(
            new QueryModel<ProjectEntity>()
            {
                Filters =
                {
                    p => p.Id == projectId && p.CreatorId == reqUser.Id && p.DeletedAt == null
                }
            }
        );
    }
}
