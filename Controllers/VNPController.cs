using PlcBase.Extensions.Utilities;
using Microsoft.AspNetCore.Mvc;
using PlcBase.Common.Constants;
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
    public BaseResponse<VNPHistory> CreatePayment(VNPPaymentInformation payment)
    {
        payment.CustomerIpAddress = HttpContext.GetIpAddress();
        Tuple<string, VNPHistory> result = _vnpHelper.CreatePayment(payment);
        return HttpContext.Success(result.Item2, HttpCode.OK, result.Item1);
    }
}