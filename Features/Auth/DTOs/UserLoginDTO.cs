using System.ComponentModel.DataAnnotations;

namespace PlcBase.Features.Auth.DTOs;

public class UserLoginDTO
{
    [Required(ErrorMessage = "email_required")]
    [EmailAddress(ErrorMessage = "invalid_email_format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "password_required")]
    public string Password { get; set; }
}