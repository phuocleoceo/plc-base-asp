using AutoMapper;

using PlcBase.Features.ProjectAccess.Entities;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Shared.Constants;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Error;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Services;

public class MemberRoleService : IMemberRoleService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public MemberRoleService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<MemberRoleDTO>> GetProjectRoleForMember(int projectMemberId)
    {
        return await _uow.MemberRole.GetManyAsync<MemberRoleDTO>(
            new QueryModel<MemberRoleEntity>()
            {
                Filters = { mr => mr.ProjectMemberId == projectMemberId }
            }
        );
    }

    public async Task<bool> CreateMemberRole(CreateMemberRoleDTO createMemberRoleDTO)
    {
        MemberRoleEntity memberRoleEntity = _mapper.Map<MemberRoleEntity>(createMemberRoleDTO);

        _uow.MemberRole.Add(memberRoleEntity);
        return await _uow.Save();
    }

    public async Task<bool> DeleteMemberRole(int projectMemberId, int projectRoleId)
    {
        MemberRoleEntity memberRoleDb = await _uow.MemberRole.GetOneAsync<MemberRoleEntity>(
            new QueryModel<MemberRoleEntity>()
            {
                Filters =
                {
                    mr => mr.ProjectMemberId == projectMemberId && mr.ProjectRoleId == projectRoleId
                }
            }
        );

        if (memberRoleDb == null)
            throw new BaseException(HttpCode.NOT_FOUND, "member_role_not_found");

        _uow.MemberRole.Remove(memberRoleDb);
        return await _uow.Save();
    }
}
