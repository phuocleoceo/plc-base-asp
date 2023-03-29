namespace PlcBase.Repositories.Interface;

public interface IUnitOfWork : IDisposable
{
    IAddressProvinceRepository AddressProvince { get; }
    IAddressDistrictRepository AddressDistrict { get; }
    IAddressWardRepository AddressWard { get; }

    Task<int> Save();
}