using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.Payment.Services;
using PlcBase.Features.Payment.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Payment.Controllers;

public class PaymentController : BaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public BaseResponse<string> CreatePayment([FromBody] CreatePaymentDTO createPaymentDTO)
    {
        ReqUser reqUser = HttpContext.GetRequestUser();
        return HttpContext.Success(_paymentService.CreatePayment(reqUser, createPaymentDTO));
    }

    [HttpPut]
    public async Task<BaseResponse<bool>> SubmitPayment(
        [FromBody] SubmitPaymentDTO submitPaymentDTO
    )
    {
        ReqUser reqUser = HttpContext.GetRequestUser();

        if (await _paymentService.SubmitPayment(reqUser, submitPaymentDTO))
            return HttpContext.Success(true);
        return HttpContext.Failure();
    }
}