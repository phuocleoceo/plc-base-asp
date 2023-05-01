using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Invitation.DTOs;

public class SenderInvitationParams : ReqParam
{
    public bool StillValid { get; set; } = true;
}
