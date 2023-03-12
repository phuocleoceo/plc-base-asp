namespace PlcBase.Helpers;

public interface IVNPHelper
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);

    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}