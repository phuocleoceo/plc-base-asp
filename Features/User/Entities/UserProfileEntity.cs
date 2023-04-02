using System.ComponentModel.DataAnnotations.Schema;

using PlcBase.Features.Address.Entities;
using PlcBase.Shared.Enums;
using PlcBase.Base.Entity;

namespace PlcBase.Features.User.Entities;

[Table(TableName.USER_PROFILE)]
public class UserProfileEntity : BaseEntity
{
    [Column("display_name")]
    public string DisplayName { get; set; }

    [Column("phone_number")]
    public string PhoneNumber { get; set; }

    [Column("identity_number")]
    public string IdentityNumber { get; set; }

    [Column("avatar")]
    public string Avatar { get; set; }

    [Column("current_credit")]
    public double CurrentCredit { get; set; }

    [Column("address")]
    public string Address { get; set; }

    [ForeignKey(nameof(AddressWard))]
    [Column("address_ward_id")]
    public int AddressWardId { get; set; }
    public AddressWardEntity AddressWard { get; set; }

    [ForeignKey(nameof(UserAccount))]
    [Column("user_id")]
    public int UserAccountId { get; set; }
    public UserAccountEntity UserAccount { get; set; }
}