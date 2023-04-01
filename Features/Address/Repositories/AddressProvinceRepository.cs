using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Features.Address.Repositories;

public class AddressProvinceRepository : BaseRepository<AddressProvinceEntity>, IAddressProvinceRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public AddressProvinceRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}