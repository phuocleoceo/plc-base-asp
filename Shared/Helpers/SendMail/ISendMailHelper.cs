namespace PlcBase.Shared.Helpers;

public interface ISendMailHelper
{
    Task<string> SendEmailAsync(MailContent mailContent);
}
