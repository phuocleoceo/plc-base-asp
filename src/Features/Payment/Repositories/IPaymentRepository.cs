using PlcBase.Features.Payment.Entities;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Payment.Repositories;

public interface IPaymentRepository : IBaseRepository<PaymentEntity>
{
    Task<PaymentEntity> GetByTxnRef(int userId, long txnRef);
}
