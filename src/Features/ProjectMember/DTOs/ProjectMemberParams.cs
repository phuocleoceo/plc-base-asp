namespace PlcBase.Features.ProjectMember.DTOs;

public class ProjectMemberParams
{
    public string SearchValue { get; set; }

    public bool WithDeleted { get; set; } = false;
}
