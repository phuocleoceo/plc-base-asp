using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Issue.Entities;

[Table(TableName.ISSUE_COMMENT)]
public class IssueCommentEntity : BaseEntity
{
    [Column("content")]
    public string Content { get; set; }

    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserAccountEntity User { get; set; }

    [ForeignKey(nameof(Issue))]
    [Column("issue_id")]
    public int IssueId { get; set; }
    public IssueEntity Issue { get; set; }
}
