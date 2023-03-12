using Microsoft.Extensions.Options;

namespace PlcBase.Helpers;

public class VNPHelper : IVNPHelper
{
    private readonly VNPSettings _vnpSettings;

    public VNPHelper(IOptions<VNPSettings> vnpSettings)
    {
        _vnpSettings = vnpSettings.Value;
    }

    public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
    {
        throw new NotImplementedException();
    }

    public PaymentResponseModel PaymentExecute(IQueryCollection collections)
    {
        throw new NotImplementedException();
    }
}