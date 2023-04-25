namespace PlcBase.Features.User.DTOs;

public class UserAccountDTO
{
    public string Email { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActived { get; set; }

    public int RoleId { get; set; }

    public string RoleName { get; set; }
}
