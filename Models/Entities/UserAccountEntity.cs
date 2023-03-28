using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PlcBase.Common.Enums;
using PlcBase.Base.Entity;


namespace PlcBase.Models.Entities;

[Table(TableName.USER_ACCOUNT)]
public class UserAccountEntity : BaseEntity
{
    [Key] 
    [Column("id")] 
    public int Id { get; set; }

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

    [ForeignKey(nameof(Role))]
    [Column("role_id")]
    public int RoleId { get; set; }

    public RoleEntity Role { get; set; }

    public UserProfileEntity UserProfile { get; set; }
}