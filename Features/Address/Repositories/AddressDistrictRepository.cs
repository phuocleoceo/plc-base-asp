using AutoMapper;

using PlcBase.Features.Address.Entities;
using PlcBase.Shared.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Address.Repositories;

public class AddressDistrictRepository : BaseRepository<AddressDistrictEntity>, IAddressDistrictRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public AddressDistrictRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}