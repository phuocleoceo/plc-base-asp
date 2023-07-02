using PlcBase.Features.Payment.Entities;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Payment.Repositories;

public interface IPaymentRepository : IBaseRepository<PaymentEntity> { }
