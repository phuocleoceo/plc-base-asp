using PlcBase.Features.AccessControl.Services;
using PlcBase.Features.ConfigSetting.Services;
using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectMember.Services;
using PlcBase.Features.ProjectStatus.Services;
using PlcBase.Features.Invitation.Services;
using PlcBase.Features.Address.Services;
using PlcBase.Features.Project.Services;
using PlcBase.Features.Sprint.Services;
using PlcBase.Features.Event.Services;
using PlcBase.Features.Issue.Services;
using PlcBase.Features.Media.Services;
using PlcBase.Features.Auth.Services;
using PlcBase.Features.User.Services;

namespace PlcBase.Common.Services;

public static class ServiceDIExtension
{
    public static void ConfigureServiceDI(this IServiceCollection services)
    {
        services.AddScoped<IAccessControlService, AccessControlService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IConfigSettingService, ConfigSettingService>();
        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectMemberService, ProjectMemberService>();
        services.AddScoped<IProjectStatusService, ProjectStatusService>();
        services.AddScoped<IInvitationService, InvitationService>();
        services.AddScoped<ISprintService, SprintService>();
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIssueCommentService, IssueCommentService>();
        services.AddScoped<IProjectRoleService, ProjectRoleService>();
        services.AddScoped<IMemberRoleService, MemberRoleService>();
        services.AddScoped<IEventService, EventService>();
    }
}
