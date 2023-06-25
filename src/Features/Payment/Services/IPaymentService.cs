using PlcBase.Features.Payment.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Payment.Services;

public interface IPaymentService
{
    string CreatePayment(ReqUser reqUser, CreatePaymentDTO createPaymentDTO);

    Task<bool> SubmitPayment(ReqUser reqUser, SubmitPaymentDTO submitPaymentDTO);
}
