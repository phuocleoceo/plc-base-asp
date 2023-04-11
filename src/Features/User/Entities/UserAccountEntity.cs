using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.AccessControl.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.User.Entities;

[Table(TableName.USER_ACCOUNT)]
public class UserAccountEntity : BaseEntity
{
    [Column("email")]
    public string Email { get; set; }

    [Column("password_hashed")]
    public byte[] PasswordHashed { get; set; }

    [Column("password_salt")]
    public byte[] PasswordSalt { get; set; }

    [Column("is_verified")]
    public bool IsVerified { get; set; }

    [Column("security_code")]
    public string SecurityCode { get; set; }

    [Column("is_actived")]
    public bool IsActived { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("refresh_token_expired_at")]
    public DateTime? RefreshTokenExpiredAt { get; set; }

    [ForeignKey(nameof(Role))]
    [Column("role_id")]
    public int RoleId { get; set; }

    public RoleEntity Role { get; set; }

    public UserProfileEntity UserProfile { get; set; }
}
