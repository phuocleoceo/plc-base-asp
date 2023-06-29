using Microsoft.EntityFrameworkCore;
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

    public async Task<List<int>> GetProjectIdsForUser(int userId)
    {
        return await _dbSet
            .Where(m => m.UserId == userId && m.DeletedAt == null)
            .Select(m => m.ProjectId)
            .ToListAsync();
    }

    public async Task SoftDeleteMemberForProject(int projectId)
    {
        await _dbSet
            .Where(m => m.ProjectId == projectId && m.DeletedAt == null)
            .ForEachAsync(m => SoftDelete(m));
    }

    public async Task<IEnumerable<string>> GetPermissionsInProjectForUser(int userId, int projectId)
    {
        List<ProjectMemberEntity> projectMembers = await _dbSet
            .Where(pm => pm.UserId == userId && pm.ProjectId == projectId)
            .Include(pm => pm.MemberRoles)
            .ThenInclude(mr => mr.ProjectRole)
            .ThenInclude(pr => pr.ProjectPermissions)
            .ToListAsync();

        return projectMembers
            .SelectMany(pm => pm.MemberRoles)
            .Select(pm => pm.ProjectRole)
            .SelectMany(pr => pr.ProjectPermissions)
            .Select(pp => pp.Key)
            .ToHashSet();
    }
}
