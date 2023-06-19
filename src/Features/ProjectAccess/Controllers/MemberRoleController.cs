using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Controllers;

public class MemberRoleController : BaseController
{
    private readonly IMemberRoleService _memberRoleService;

    public MemberRoleController(IMemberRoleService memberRoleService)
    {
        _memberRoleService = memberRoleService;
    }
}
