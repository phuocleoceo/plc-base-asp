using AutoMapper;

using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Features.Issue.DTOs;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Issue.Services;

public class IssueService : IIssueService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public IssueService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<IssueDTO>> GetIssuesInBacklog(int projectId)
    {
        return await _uow.Issue.GetManyAsync<IssueDTO>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderBy(i => i.BacklogIndex),
                Filters =
                {
                    i => i.ProjectId == projectId && i.BacklogIndex != null && i.SprintId == null
                },
                Includes =
                {
                    i => i.Assignee.UserProfile,
                    i => i.Reporter.UserProfile,
                    i => i.ProjectStatus
                },
            }
        );
    }

    public async Task<List<IssueDTO>> GetIssuesInSprint(int projectId, int sprintId)
    {
        return await _uow.Issue.GetManyAsync<IssueDTO>(
            new QueryModel<IssueEntity>()
            {
                OrderBy = c => c.OrderByDescending(i => i.CreatedAt),
                Filters =
                {
                    i =>
                        i.ProjectId == projectId && i.SprintId == sprintId && i.BacklogIndex == null
                },
                Includes =
                {
                    i => i.Assignee.UserProfile,
                    i => i.Reporter.UserProfile,
                    i => i.ProjectStatus
                },
            }
        );
    }
}
