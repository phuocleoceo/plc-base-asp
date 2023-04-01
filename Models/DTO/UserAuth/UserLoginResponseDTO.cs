namespace PlcBase.Models.DTO;

public class UserLoginResponseDTO
{
    public int Id { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; }

    public string AccessToken { get; set; }

    public DateTime TokenExpiredAt { get; set; }
}