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
    private readonly ICapPublisher _capPublisher;

    public AuthMailService(
        IOptions<ClientAppSettings> clientAppSettings,
        ICapPublisher capPublisher
    )
    {
        _clientAppSettings = clientAppSettings.Value;
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

        await SendMailBackground(
            new MailContent()
            {
                ToEmail = userAccount.Email,
                Subject = "Confirm email to use PLC Base",
                Body =
                    $"Confirm the registration by clicking on the <a href='{uriBuilder}'>link</a>"
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

        await SendMailBackground(
            new MailContent()
            {
                ToEmail = userAccount.Email,
                Subject = "Password recovery for your PLC Base account",
                Body = $"Clicking on the <a href='{uriBuilder}'>link</a> to recover your password"
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
