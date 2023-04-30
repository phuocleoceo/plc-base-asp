using PlcBase.Features.Invitation.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Invitation.Services;

public interface IInvitationService
{
    Task<bool> CreateInvitaion(
        ReqUser reqUser,
        int projectId,
        CreateInvitationDTO createInvitationDTO
    );

    Task<bool> DeleteInvitation(ReqUser reqUser, int projectId, int invitationId);

    Task<bool> AcceptInvitation(ReqUser reqUser, int invitationId);

    Task<bool> DeclineInvitation(ReqUser reqUser, int invitationId);
}
