using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.ProjectStatus.Entities;
using PlcBase.Features.Project.Entities;
using PlcBase.Features.Sprint.Entities;
using PlcBase.Features.User.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.Invitation.Entities;

[Table(TableName.INVITATION)]
public class InvitationEntity : BaseEntity { }
