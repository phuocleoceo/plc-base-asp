using System.Collections.Specialized;
using Microsoft.Extensions.Options;
using DotNetCore.CAP;
using System.Web;

using PlcBase.Features.User.Entities;
using PlcBase.Shared.Helpers;
using PlcBase.Shared.Enums;

namespace PlcBase.Features.Auth.Services;

public class AuthMailService : IAuthMailService
{
    private readonly ClientAppSettings _clientAppSettings;
    private readonly MailSettings _mailSettings;
    private readonly ICapPublisher _capPublisher;

    public AuthMailService(
        IOptions<ClientAppSettings> clientAppSettings,
        IOptions<MailSettings> mailSettings,
        ICapPublisher capPublisher
    )
    {
        _clientAppSettings = clientAppSettings.Value;
        _mailSettings = mailSettings.Value;
        _capPublisher = capPublisher;
    }

    public async Task SendMailConfirm(UserAccountEntity userAccount)
    {
        string webClientPath =
            $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.ConfirmEmailPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = userAccount.Id.ToString();
        query["code"] = userAccount.SecurityCode;
        uriBuilder.Query = query.ToString() ?? string.Empty;

        string body;
        string path = Path.Combine(_mailSettings.Templates, "MailConfirm.html");
        using (StreamReader reader = new StreamReader(path))
        {
            body = await reader.ReadToEndAsync();
        }
        body = body.Replace("{confirm-link}", uriBuilder.ToString());

        await SendMailBackground(
            new MailContent()
            {
                ToEmail = userAccount.Email,
                Subject = "Confirm email to use Ji PLC",
                Body = body
            }
        );
    }

    public async Task SendMailRecoverPassword(UserAccountEntity userAccount)
    {
        string webClientPath =
            $"{_clientAppSettings.EndUserAppUrl}/{_clientAppSettings.RecoverPasswordPath}";
        UriBuilder uriBuilder = new UriBuilder(webClientPath);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["userId"] = userAccount.Id.ToString();
        query["code"] = userAccount.SecurityCode;
        uriBuilder.Query = query.ToString() ?? string.Empty;

        string body;
        string path = Path.Combine(_mailSettings.Templates, "MailConfirm.html");
        using (StreamReader reader = new StreamReader(path))
        {
            body = await reader.ReadToEndAsync();
        }
        body = body.Replace("{recover-link}", uriBuilder.ToString());

        await SendMailBackground(
            new MailContent()
            {
                ToEmail = userAccount.Email,
                Subject = "Password recovery for your PLC Base account",
                Body = body
            }
        );
    }

    private async Task SendMailBackground(MailContent mailContent)
    {
        try
        {
            await _capPublisher.PublishAsync(WorkerType.SEND_MAIL, mailContent);
        }
        catch
        {
            // ignored
        }
    }
}
