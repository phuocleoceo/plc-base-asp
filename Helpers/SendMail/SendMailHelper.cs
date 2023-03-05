using Microsoft.Extensions.Options;
using PlcBase.Common.Constants;
using PlcBase.Base.Error;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PlcBase.Helpers;

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
            byte[] fileBytes;
            foreach (IFormFile file in mailContent.Attachments)
            {
                if (file.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }
        }

        builder.HtmlBody = mailContent.Body;
        email.Body = builder.ToMessageBody();

        using SmtpClient smtp = new SmtpClient();

        try
        {
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);

            return $"Send mail to {mailContent.ToEmail}";
        }
        catch (Exception ex)
        {
            throw new BaseException
            (
                HttpCode.BAD_REQUEST,
                $"Error when sending email to {mailContent.ToEmail} with reason {ex.Message}"
            );
        }
        finally
        {
            smtp.Disconnect(true);
        }
    }
}