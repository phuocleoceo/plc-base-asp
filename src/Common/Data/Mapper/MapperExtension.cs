using PlcBase.Features.AccessControl.DTOs;
using PlcBase.Features.ConfigSetting.DTOs;
using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Features.ProjectStatus.DTOs;
using PlcBase.Features.Invitation.DTOs;
using PlcBase.Features.Address.DTOs;
using PlcBase.Features.Project.DTOs;
using PlcBase.Features.Sprint.DTOs;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Features.Media.DTOs;
using PlcBase.Features.Auth.DTOs;
using PlcBase.Features.User.DTOs;

namespace PlcBase.Common.Data.Mapper;

public static class MapperExtension
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AuthMapping));
        services.AddAutoMapper(typeof(UserMapping));
        services.AddAutoMapper(typeof(AddressMapping));
        services.AddAutoMapper(typeof(RolePermissionMapping));
        services.AddAutoMapper(typeof(ConfigSettingMapping));
        services.AddAutoMapper(typeof(MediaMapping));
        services.AddAutoMapper(typeof(ProjectMapping));
        services.AddAutoMapper(typeof(ProjectMemberMapping));
        services.AddAutoMapper(typeof(ProjectStatusMapping));
        services.AddAutoMapper(typeof(InvitationMapping));
        services.AddAutoMapper(typeof(SprintMapping));
        services.AddAutoMapper(typeof(IssueMapping));
    }
}
