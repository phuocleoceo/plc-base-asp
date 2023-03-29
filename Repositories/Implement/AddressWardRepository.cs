using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Repositories.Implement;

public class AddressWardRepository : BaseRepository<AddressWardEntity>, IAddressWardRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public AddressWardRepository(DataContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}