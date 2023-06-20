using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.ProjectAccess.Entities;

[Table(TableName.MEMBER_ROLE)]
public class MemberRoleEntity : BaseEntity
{
    [ForeignKey(nameof(ProjectMember))]
    [Column("project_member_id")]
    public int ProjectMemberId { get; set; }
    public ProjectMemberEntity ProjectMember { get; set; }

    [ForeignKey(nameof(ProjectRole))]
    [Column("project_role_id")]
    public int ProjectRoleId { get; set; }
    public ProjectRoleEntity ProjectRole { get; set; }
}
