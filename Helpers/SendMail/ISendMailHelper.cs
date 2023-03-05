namespace PlcBase.Helpers;

public interface ISendMailHelper
{
    Task<string> SendEmailAsync(MailContent mailContent);
}