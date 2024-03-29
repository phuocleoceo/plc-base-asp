using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Features.Address.DTOs;
using PlcBase.Features.Payment.DTOs;
using PlcBase.Features.Project.DTOs;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Features.Event.DTOs;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Features.Media.DTOs;
using PlcBase.Features.Auth.DTOs;
using PlcBase.Features.User.DTOs;

namespace PlcBase.Common.Data.Mapper;

public static class MapperExtension
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(AuthMapping),
            typeof(UserMapping),
            typeof(AddressMapping),
            typeof(AccessControlMapping),
            typeof(ConfigSettingMapping),
            typeof(MediaMapping),
            typeof(ProjectMapping),
            typeof(ProjectMemberMapping),
            typeof(ProjectStatusMapping),
            typeof(InvitationMapping),
            typeof(SprintMapping),
            typeof(IssueMapping),
            typeof(IssueCommentMapping),
            typeof(ProjectRoleMapping),
            typeof(ProjectPermissionMapping),
            typeof(MemberRoleMapping),
            typeof(EventMapping),
            typeof(PaymentMapping)
        );
    }
}
