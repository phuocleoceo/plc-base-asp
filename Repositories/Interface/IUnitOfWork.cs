namespace PlcBase.Repositories.Interface;

public interface IUnitOfWork : IDisposable
{
    IAddressProvinceRepository AddressProvince { get; }
    IAddressDistrictRepository AddressDistrict { get; }
    IAddressWardRepository AddressWard { get; }
    IUserAccountRepository UserAccount { get; }
    IUserProfileRepository UserProfile { get; }
    IRoleRepository Role { get; }
    IPermisisonRepository Permisison { get; }

    Task<int> Save();
    Task CreateTransaction();
    Task CommitTransaction();
    Task AbortTransaction();
}