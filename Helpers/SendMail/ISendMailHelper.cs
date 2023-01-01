namespace PlcBase.Helpers;

public interface ISendMailHelper
{
    Task SendEmailAsync(MailContent mailContent);
}