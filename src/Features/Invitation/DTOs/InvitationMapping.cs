using AutoMapper;

using PlcBase.Features.Invitation.Entities;

namespace PlcBase.Features.Invitation.DTOs;

public class InvitationMapping : Profile
{
    public InvitationMapping()
    {
        CreateMap<InvitationEntity, RecipientInvitationDTO>()
            .ForMember(dto => dto.InvitationId, prop => prop.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.ProjectName, prop => prop.MapFrom(entity => entity.Project.Name))
            .ForMember(dto => dto.SenderEmail, prop => prop.MapFrom(entity => entity.Sender.Email))
            .ForMember(
                dto => dto.SenderName,
                prop => prop.MapFrom(entity => entity.Sender.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.SenderAvatar,
                prop => prop.MapFrom(entity => entity.Sender.UserProfile.Avatar)
            );

        CreateMap<InvitationEntity, SenderInvitationDTO>()
            .ForMember(dto => dto.InvitationId, prop => prop.MapFrom(entity => entity.Id))
            .ForMember(
                dto => dto.RecipientEmail,
                prop => prop.MapFrom(entity => entity.Recipient.Email)
            )
            .ForMember(
                dto => dto.RecipientName,
                prop => prop.MapFrom(entity => entity.Recipient.UserProfile.DisplayName)
            )
            .ForMember(
                dto => dto.RecipientAvatar,
                prop => prop.MapFrom(entity => entity.Recipient.UserProfile.Avatar)
            );
    }
}
