using AutoMapper;

using PlcBase.Features.Payment.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Payment.Repositories;

public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public PaymentRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
