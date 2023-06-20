namespace PlcBase.Features.ProjectMember.DTOs;

public class ProjectMemberDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public int ProjectMemberId { get; set; }
    public IEnumerable<string> MemberRoles { get; set; }
}
