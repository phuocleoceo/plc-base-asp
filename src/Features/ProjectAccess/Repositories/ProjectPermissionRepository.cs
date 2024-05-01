using Microsoft.EntityFrameworkCore;
using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

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

    public async Task<List<ProjectPermissionEntity>> GetForProjectRole(int projectRoleId)
    {
        return await GetManyAsync<ProjectPermissionEntity>(
            new QueryModel<ProjectPermissionEntity>()
            {
                Filters = { pm => pm.ProjectRoleId == projectRoleId }
            }
        );
    }

    public async Task<IEnumerable<string>> GetPermissionKeysOfRole(int projectRoleId)
    {
        return await _db.ProjectPermissions
            .Where(pm => pm.ProjectRoleId == projectRoleId)
            .Select(pm => pm.Key)
            .ToListAsync();
    }
}
