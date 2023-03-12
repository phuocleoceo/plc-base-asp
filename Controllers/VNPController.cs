using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;
using PlcBase.Helpers;

namespace PlcBase.Controllers;

public class VNPController : BaseController
{
    private readonly IVNPHelper _vnpHelper;
    public VNPController(IVNPHelper vnpHelper)
    {
        _vnpHelper = vnpHelper;
    }

    [HttpPost("Create")]
    public BaseResponse<string> CreatePaymentUrl(PaymentInformationModel model)
    {
        string url = _vnpHelper.CreatePaymentUrl(model, HttpContext);
        return HttpContext.Success(url);
    }

    [HttpGet("Callback")]
    public BaseResponse<PaymentResponseModel> PaymentCallback()
    {
        PaymentResponseModel result = _vnpHelper.PaymentExecute(Request.Query);
        return HttpContext.Success(result);
    }
}