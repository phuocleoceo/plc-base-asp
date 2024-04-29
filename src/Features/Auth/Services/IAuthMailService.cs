using PlcBase.Features.User.Entities;

namespace PlcBase.Features.Auth.Services;

public interface IAuthMailService
{
    Task SendMailConfirm(UserAccountEntity userAccount, UserProfileEntity userProfile);

    Task SendMailRecoverPassword(UserAccountEntity userAccount, UserProfileEntity userProfile);
}
