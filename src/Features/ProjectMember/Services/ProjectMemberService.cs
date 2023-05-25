using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;

namespace PlcBase.Features.ProjectMember.Services;

public class ProjectMemberService : IProjectMemberService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProjectMemberService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<ProjectMemberDTO>> GetMembersForProject(
        int projectId,
        ProjectMemberParams projectMemberParams
    )
    {
        QueryModel<ProjectMemberEntity> memberQuery = new QueryModel<ProjectMemberEntity>()
        {
            OrderBy = c => c.OrderByDescending(up => up.CreatedAt),
            Filters = { i => i.ProjectId == projectId },
            Includes = { i => i.User.UserProfile, },
        };

        if (!projectMemberParams.WithDeleted)
        {
            memberQuery.Filters.Add(i => i.DeletedAt == null);
        }

        if (!string.IsNullOrWhiteSpace(projectMemberParams.SearchValue))
        {
            memberQuery.Filters.Add(
                i =>
                    i.User.Email.ToLower().Contains(projectMemberParams.SearchValue)
                    || i.User.UserProfile.DisplayName
                        .ToLower()
                        .Contains(projectMemberParams.SearchValue)
            );
        }

        return await _uow.ProjectMember.GetManyAsync<ProjectMemberDTO>(memberQuery);
    }

    public async Task<bool> DeleteProjectMember(int projectId, int projectMemberId)
    {
        ProjectMemberEntity projectMemberDb = await _uow.ProjectMember.FindByIdAsync(
            projectMemberId
        );

        if (projectMemberDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "project_member_not_found");

        if (projectMemberDb.ProjectId != projectId)
            throw new BaseException(HttpCode.BAD_REQUEST, "invalid_project_member");

        _uow.ProjectMember.SoftDelete(projectMemberDb);
        return await _uow.Save();
    }
}
