using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectMember.Entities;

[Table(TableName.PROJECT_MEMBER)]
public class ProjectMemberEntity : BaseSoftDeletableEntity
{
    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserAccountEntity User { get; set; }

    [ForeignKey(nameof(Project))]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; }

    public ICollection<MemberRoleEntity> MemberRoles { get; set; }

    public ProjectMemberEntity()
    {
        MemberRoles = new List<MemberRoleEntity>();
    }
}
