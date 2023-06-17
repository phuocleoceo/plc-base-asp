using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

using PlcBase.Common.Data.Context.Configuration;
using PlcBase.Features.AccessControl.Entities;
using PlcBase.Features.ConfigSetting.Entities;
using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.Invitation.Entities;
using PlcBase.Features.Address.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.Issue.Entities;
using PlcBase.Features.Media.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Base.Entity;

namespace PlcBase.Common.Data.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntityConfigurations();
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<AddressDistrictEntity> AddressDistricts { get; set; }
    public DbSet<AddressProvinceEntity> AddressProvinces { get; set; }
    public DbSet<AddressWardEntity> AddressWards { get; set; }
    public DbSet<UserAccountEntity> UserAccounts { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<ConfigSettingEntity> ConfigSettings { get; set; }
    public DbSet<MediaEntity> Medias { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectMemberEntity> ProjectMembers { get; set; }
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; }
    public DbSet<InvitationEntity> Invitations { get; set; }
    public DbSet<SprintEntity> Sprints { get; set; }
    public DbSet<IssueEntity> Issues { get; set; }
    public DbSet<IssueCommentEntity> IssueComments { get; set; }
    public DbSet<ProjectRoleEntity> ProjectRoles { get; set; }
    public DbSet<ProjectPermissionEntity> ProjectPermissions { get; set; }
}
