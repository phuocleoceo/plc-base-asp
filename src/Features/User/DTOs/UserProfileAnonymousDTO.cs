namespace PlcBase.Features.User.DTOs;

public class UserProfileAnonymousDTO
{
    public int Id { get; set; }
    public int UserAccountId { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Avatar { get; set; }
    public string Address { get; set; }
    public int AddressWardId { get; set; }
    public string AddressWard { get; set; }
    public string AddressDistrict { get; set; }
    public string AddressProvince { get; set; }
}
