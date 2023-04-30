using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.Invitation.Entities;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

namespace PlcBase.Features.Invitation.Services;

public class InvitationService : IInvitationService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InvitationService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<bool> CreateInvitaion(
        ReqUser reqUser,
        int projectId,
        CreateInvitationDTO createInvitationDTO
    )
    {
        if (reqUser.Id == createInvitationDTO.RecipientId)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_invitation");

        InvitationEntity invitationEntity = _mapper.Map<InvitationEntity>(createInvitationDTO);
        invitationEntity.ProjectId = projectId;
        invitationEntity.SenderId = reqUser.Id;

        _uow.Invitation.Add(invitationEntity);
        return await _uow.Save();
    }

    public async Task<bool> DeleteInvitation(ReqUser reqUser, int projectId, int invitationId)
    {
        InvitationEntity invitationDb = await _uow.Invitation.FindByIdAsync(invitationId);

        if (invitationDb.ProjectId != projectId || invitationDb.SenderId != reqUser.Id)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_invitation");

        _uow.Invitation.Remove(invitationDb);
        return await _uow.Save();
    }

    public async Task<bool> AcceptInvitation(ReqUser reqUser, int invitationId)
    {
        InvitationEntity invitationDb = await _uow.Invitation.FindByIdAsync(invitationId);

        if (invitationDb.RecipientId != reqUser.Id)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_invitation");

        invitationDb.AcceptedAt = DateTime.UtcNow;
        invitationDb.DeclinedAt = null;
        _uow.Invitation.Update(invitationDb);

        _uow.ProjectMember.Add(
            new ProjectMemberEntity()
            {
                UserId = invitationDb.RecipientId,
                ProjectId = invitationDb.ProjectId,
            }
        );

        return await _uow.Save();
    }

    public async Task<bool> DeclineInvitation(ReqUser reqUser, int invitationId)
    {
        InvitationEntity invitationDb = await _uow.Invitation.FindByIdAsync(invitationId);

        if (invitationDb.RecipientId != reqUser.Id)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_invitation");

        invitationDb.AcceptedAt = null;
        invitationDb.DeclinedAt = DateTime.UtcNow;
        _uow.Invitation.Update(invitationDb);
        return await _uow.Save();
    }
}
