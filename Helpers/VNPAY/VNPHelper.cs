using Microsoft.Extensions.Options;

namespace PlcBase.Helpers;

public class VNPHelper : IVNPHelper
{
    private readonly IDateTimeHelper _dateTimeHelper;
    private readonly VNPSettings _vnpSettings;

    public VNPHelper(IDateTimeHelper dateTimeHelper,
                     IOptions<VNPSettings> vnpSettings)
    {
        _dateTimeHelper = dateTimeHelper;
        _vnpSettings = vnpSettings.Value;
    }

    public string CreatePaymentUrl(PaymentInformationModel paymentModel, HttpContext context)
    {
        VnPayLibrary pay = new VnPayLibrary();

        DateTime timeNow = _dateTimeHelper.Now();
        string tick = DateTime.Now.Ticks.ToString();
        string orderInfo = $"{paymentModel.Name} {paymentModel.OrderDescription} {paymentModel.Amount}";

        pay.AddRequestData("vnp_Version", _vnpSettings.Version);
        pay.AddRequestData("vnp_Command", _vnpSettings.Command);
        pay.AddRequestData("vnp_TmnCode", _vnpSettings.TmnCode);
        pay.AddRequestData("vnp_Amount", ((int)paymentModel.Amount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
        pay.AddRequestData("vnp_CurrCode", _vnpSettings.CurrCode);
        pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
        pay.AddRequestData("vnp_Locale", _vnpSettings.Locale);
        pay.AddRequestData("vnp_OrderInfo", orderInfo);
        pay.AddRequestData("vnp_OrderType", paymentModel.OrderType);
        pay.AddRequestData("vnp_ReturnUrl", _vnpSettings.CallbackUrl);
        pay.AddRequestData("vnp_TxnRef", tick);

        string paymentUrl = pay.CreateRequestUrl(_vnpSettings.GatewayUrl, _vnpSettings.HashSecret);

        return paymentUrl;
    }

    public PaymentResponseModel PaymentExecute(IQueryCollection collections)
    {
        VnPayLibrary pay = new VnPayLibrary();
        return pay.GetFullResponseData(collections, _vnpSettings.HashSecret);
    }
}