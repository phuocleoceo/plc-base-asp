namespace PlcBase.Features.Auth.DTOs;

public class UserRefreshTokenResponseDTO
{
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpiredAt { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpiredAt { get; set; }
}
