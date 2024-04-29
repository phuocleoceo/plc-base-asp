using PlcBase.Features.User.Entities;

namespace PlcBase.Features.Auth.Services;

public interface IAuthMailService
{
    Task SendMailConfirm(UserAccountEntity userAccount);

    Task SendMailRecoverPassword(UserAccountEntity userAccount);
}
