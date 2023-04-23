using PlcBase.Features.AccessControl.Repositories;
using PlcBase.Features.ConfigSetting.Repositories;
using PlcBase.Features.ProjectMember.Repositories;
using PlcBase.Features.Address.Repositories;
using PlcBase.Features.Project.Repositories;
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

    Task<int> Save();
    Task CreateTransaction();
    Task CommitTransaction();
    Task AbortTransaction();
}
