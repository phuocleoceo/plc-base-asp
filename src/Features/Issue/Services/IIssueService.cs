using PlcBase.Features.Issue.DTOs;

namespace PlcBase.Features.Issue.Services;

public interface IIssueService
{
    Task<List<IssueDTO>> GetIssuesInBacklog(int projectId);

    Task<List<IssueDTO>> GetIssuesInSprint(int projectId, int sprintId);
}
