namespace PlcBase.Shared.Helpers;

public class VNPPaymentInformation
{
    public string BankCode { get; set; }

    public long Amount { get; set; }

    public string OrderType { get; set; }

    public string OrderDescription { get; set; }

    public string CustomerIpAddress { get; set; }
}
