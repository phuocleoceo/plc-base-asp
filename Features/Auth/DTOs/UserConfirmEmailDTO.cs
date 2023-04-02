namespace PlcBase.Features.Auth.DTOs;

public class UserConfirmEmailDTO
{
    public int UserId { get; set; }

    public string Code { get; set; }
}