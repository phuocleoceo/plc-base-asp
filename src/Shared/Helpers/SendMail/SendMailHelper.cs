using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Shared.Helpers;

public class SendMailHelper : ISendMailHelper
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<SendMailHelper> _logger;

    public SendMailHelper(IOptions<MailSettings> mailSettings, ILogger<SendMailHelper> logger)
    {
        _mailSettings = mailSettings.Value;
        _logger = logger;
    }

    public async Task<string> SendEmailAsync(MailContent mailContent)
    {
        MimeMessage email = new MimeMessage();
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        email.To.Add(MailboxAddress.Parse(mailContent.ToEmail));
        email.Subject = mailContent.Subject;

        BodyBuilder builder = new BodyBuilder();

        if (mailContent.Attachments != null)
        {
            foreach (IFormFile file in mailContent.Attachments.Where(file => file.Length > 0))
            {
                byte[] fileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                builder.Attachments.Add(
                    file.FileName,
                    fileBytes,
                    ContentType.Parse(file.ContentType)
                );
            }
        }

        builder.HtmlBody = mailContent.Body;
        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new SmtpClient();

        try
        {
            await smtp.ConnectAsync(
                _mailSettings.Host,
                _mailSettings.Port,
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);

            return $"Send mail to {mailContent.ToEmail}";
        }
        catch (Exception ex)
        {
            throw new BaseException(
                HttpCode.BAD_REQUEST,
                $"Error when sending email to {mailContent.ToEmail} with reason {ex.Message}"
            );
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}
