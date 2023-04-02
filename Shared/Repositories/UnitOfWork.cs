using PlcBase.Features.AccessControl.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using PlcBase.Features.Address.Repositories;
using PlcBase.Features.User.Repositories;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Shared.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;

        AddressProvince = new AddressProvinceRepository(_db, _mapper);
        AddressDistrict = new AddressDistrictRepository(_db, _mapper);
        AddressWard = new AddressWardRepository(_db, _mapper);
        UserAccount = new UserAccountRepository(_db, _mapper);
        UserProfile = new UserProfileRepository(_db, _mapper);
        Role = new RoleRepository(_db, _mapper);
        Permisison = new PermissionRepository(_db, _mapper);
    }

    public IAddressProvinceRepository AddressProvince { get; private set; }
    public IAddressDistrictRepository AddressDistrict { get; private set; }
    public IAddressWardRepository AddressWard { get; private set; }
    public IUserAccountRepository UserAccount { get; private set; }
    public IUserProfileRepository UserProfile { get; private set; }
    public IRoleRepository Role { get; private set; }
    public IPermisisonRepository Permisison { get; private set; }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task<int> Save()
    {
        return await _db.SaveChangesAsync();
    }

    public async Task CreateTransaction()
    {
        if (_transaction == null)
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransaction()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task AbortTransaction()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}