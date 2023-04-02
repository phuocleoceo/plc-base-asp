using Microsoft.AspNetCore.Mvc;
using PlcBase.Shared.Utilities;
using PlcBase.Shared.Constants;
using PlcBase.Base.Controller;
using PlcBase.Shared.Helpers;
using PlcBase.Base.DTO;

namespace PlcBase.Features.Test.Controllers;

public class VNPController : BaseController
{
    private readonly IVNPHelper _vnpHelper;
    public VNPController(IVNPHelper vnpHelper)
    {
        _vnpHelper = vnpHelper;
    }

    [HttpPost("Create")]
    public BaseResponse<VNPHistory> CreatePayment(VNPPaymentInformation payment)
    {
        payment.CustomerIpAddress = HttpContext.GetIpAddress();
        Tuple<string, VNPHistory> result = _vnpHelper.CreatePayment(payment);
        return HttpContext.Success(result.Item2, HttpCode.OK, result.Item1);
    }
}