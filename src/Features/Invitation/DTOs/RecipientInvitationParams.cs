using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Invitation.DTOs;

public class RecipientInvitationParams : ReqParam
{
    public bool StillValid { get; set; } = true;
}
