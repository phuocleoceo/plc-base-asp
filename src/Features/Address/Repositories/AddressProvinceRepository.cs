using AutoMapper;

using PlcBase.Features.Address.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Address.Repositories;

public class AddressProvinceRepository
    : BaseRepository<AddressProvinceEntity>,
        IAddressProvinceRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public AddressProvinceRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
