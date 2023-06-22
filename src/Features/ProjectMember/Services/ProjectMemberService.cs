using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Features.ProjectMember.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

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

    public async Task<PagedList<ProjectMemberDTO>> GetMembersForProject(
        int projectId,
        ProjectMemberParams projectMemberParams
    )
    {
        QueryModel<ProjectMemberEntity> memberQuery = new QueryModel<ProjectMemberEntity>()
        {
            OrderBy = c => c.OrderByDescending(pm => pm.CreatedAt),
            Filters = { pm => pm.ProjectId == projectId },
            Includes = { pm => pm.User.UserProfile },
            PageSize = projectMemberParams.PageSize,
            PageNumber = projectMemberParams.PageNumber
        };

        if (!projectMemberParams.WithDeleted)
        {
            memberQuery.Filters.Add(i => i.DeletedAt == null);
        }

        if (!string.IsNullOrWhiteSpace(projectMemberParams.SearchValue))
        {
            string searchValue = projectMemberParams.SearchValue.ToLower();
            memberQuery.Filters.Add(
                i =>
                    i.User.Email.ToLower().Contains(searchValue)
                    || i.User.UserProfile.DisplayName.ToLower().Contains(searchValue)
            );
        }

        return await _uow.ProjectMember.GetPagedAsync<ProjectMemberDTO>(memberQuery);
    }

    public async Task<List<ProjectMemberSelectDTO>> GetMembersForSelect(int projectId)
    {
        return await _uow.ProjectMember.GetManyAsync<ProjectMemberSelectDTO>(
            new QueryModel<ProjectMemberEntity>()
            {
                Filters = { i => i.ProjectId == projectId && i.DeletedAt == null },
                Includes = { i => i.User.UserProfile, },
            }
        );
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
