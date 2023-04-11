namespace PlcBase.Shared.Helpers;

public interface IVNPHelper
{
    Tuple<string, VNPHistory> CreatePayment(VNPPaymentInformation paymentInfo);
}
