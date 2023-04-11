namespace PlcBase.Features.Auth.DTOs;

public class UserLoginResponseDTO
{
    public int Id { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set; }

    public string AccessToken { get; set; }

    public DateTime AccessTokenExpiredAt { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiredAt { get; set; }
}
