using PlcBase.Features.AccessControl.Repositories;
using PlcBase.Features.ConfigSetting.Repositories;
using PlcBase.Features.ProjectMember.Repositories;
using PlcBase.Features.ProjectAccess.Repositories;
using PlcBase.Features.ProjectStatus.Repositories;
using PlcBase.Features.Invitation.Repositories;
using PlcBase.Features.Address.Repositories;
using PlcBase.Features.Project.Repositories;
using PlcBase.Features.Sprint.Repositories;
using PlcBase.Features.Issue.Repositories;
using PlcBase.Features.Media.Repositories;
using PlcBase.Features.User.Repositories;

namespace PlcBase.Common.Repositories;

public interface IUnitOfWork : IDisposable
{
    IDapperContainer DapperContainer { get; }
    IAddressProvinceRepository AddressProvince { get; }
    IAddressDistrictRepository AddressDistrict { get; }
    IAddressWardRepository AddressWard { get; }
    IUserAccountRepository UserAccount { get; }
    IUserProfileRepository UserProfile { get; }
    IRoleRepository Role { get; }
    IPermisisonRepository Permisison { get; }
    IConfigSettingRepository ConfigSetting { get; }
    IMediaRepository Media { get; }
    IProjectRepository Project { get; }
    IProjectMemberRepository ProjectMember { get; }
    IProjectStatusRepository ProjectStatus { get; }
    IInvitationRepository Invitation { get; }
    ISprintRepository Sprint { get; }
    IIssueRepository Issue { get; }
    IIssueCommentRepository IssueComment { get; }
    IProjectRoleRepository ProjectRole { get; }
    IProjectPermissionRepository ProjectPermission { get; }

    Task<bool> Save();
    Task CreateTransaction();
    Task CommitTransaction();
    Task AbortTransaction();
}
