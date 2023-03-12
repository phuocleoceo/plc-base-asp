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

    public Tuple<string, VNPHistory> CreatePayment(VNPPaymentInformation paymentInfo)
    {
        VNPHistory vnpHistory = new VNPHistory();
        vnpHistory.vnp_TxnRef = DateTime.UtcNow.Ticks;
        vnpHistory.vnp_IpAddr = paymentInfo.CustomerIpAddress;
        vnpHistory.vnp_OrderType = paymentInfo.OrderType;
        vnpHistory.vnp_OrderInfo = "#" + $"{vnpHistory.vnp_TxnRef} | {paymentInfo.OrderDescription}";
        vnpHistory.vnp_Amount = paymentInfo.Amount;
        vnpHistory.vnp_BankCode = paymentInfo.BankCode;
        vnpHistory.vnp_CreateDate = _dateTimeHelper.Now().ToString("yyyyMMddHHmmss");

        VNPLibrary pay = new VNPLibrary();

        pay.AddRequestData("vnp_Version", _vnpSettings.Version);
        pay.AddRequestData("vnp_Command", _vnpSettings.Command);
        pay.AddRequestData("vnp_TmnCode", _vnpSettings.TmnCode);
        pay.AddRequestData("vnp_CurrCode", _vnpSettings.CurrCode);
        pay.AddRequestData("vnp_Locale", _vnpSettings.Locale);
        pay.AddRequestData("vnp_ReturnUrl", _vnpSettings.ReturnUrl);
        pay.AddRequestData("vnp_IpAddr", vnpHistory.vnp_IpAddr);
        pay.AddRequestData("vnp_Amount", (vnpHistory.vnp_Amount * 100).ToString());
        pay.AddRequestData("vnp_BankCode", vnpHistory.vnp_BankCode);
        pay.AddRequestData("vnp_CreateDate", vnpHistory.vnp_CreateDate);
        pay.AddRequestData("vnp_OrderInfo", vnpHistory.vnp_OrderInfo);
        pay.AddRequestData("vnp_OrderType", vnpHistory.vnp_OrderType);
        pay.AddRequestData("vnp_TxnRef", vnpHistory.vnp_TxnRef.ToString());

        string paymentUrl = pay.CreateRequestUrl(_vnpSettings.BaseUrl, _vnpSettings.HashSecret);

        return new Tuple<string, VNPHistory>(paymentUrl, vnpHistory);
    }
}