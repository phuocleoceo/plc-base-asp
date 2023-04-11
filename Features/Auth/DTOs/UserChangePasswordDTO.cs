namespace PlcBase.Features.Auth.DTOs;

public class UserChangePasswordDTO
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}
