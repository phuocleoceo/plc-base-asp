namespace PlcBase.Features.Auth.DTOs;

public class UserRecoverPasswordDTO
{
    public int UserId { get; set; }

    public string Code { get; set; }

    public string NewPassword { get; set; }
}