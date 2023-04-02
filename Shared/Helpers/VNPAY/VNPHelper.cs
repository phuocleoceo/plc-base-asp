using Microsoft.Extensions.Options;

namespace PlcBase.Shared.Helpers;

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

        VNPLibrary vnpay = new VNPLibrary();

        vnpay.AddRequestData("vnp_Version", _vnpSettings.Version);
        vnpay.AddRequestData("vnp_Command", _vnpSettings.Command);
        vnpay.AddRequestData("vnp_TmnCode", _vnpSettings.TmnCode);
        vnpay.AddRequestData("vnp_CurrCode", _vnpSettings.CurrCode);
        vnpay.AddRequestData("vnp_Locale", _vnpSettings.Locale);
        vnpay.AddRequestData("vnp_ReturnUrl", _vnpSettings.ReturnUrl);
        vnpay.AddRequestData("vnp_IpAddr", vnpHistory.vnp_IpAddr);
        vnpay.AddRequestData("vnp_Amount", (vnpHistory.vnp_Amount * 100).ToString());
        vnpay.AddRequestData("vnp_BankCode", vnpHistory.vnp_BankCode);
        vnpay.AddRequestData("vnp_CreateDate", vnpHistory.vnp_CreateDate);
        vnpay.AddRequestData("vnp_OrderInfo", vnpHistory.vnp_OrderInfo);
        vnpay.AddRequestData("vnp_OrderType", vnpHistory.vnp_OrderType);
        vnpay.AddRequestData("vnp_TxnRef", vnpHistory.vnp_TxnRef.ToString());

        string paymentUrl = vnpay.CreateRequestUrl(_vnpSettings.BaseUrl, _vnpSettings.HashSecret);

        return new Tuple<string, VNPHistory>(paymentUrl, vnpHistory);
    }
}