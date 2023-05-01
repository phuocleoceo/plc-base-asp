using AutoMapper;

using PlcBase.Features.ProjectMember.Entities;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
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
