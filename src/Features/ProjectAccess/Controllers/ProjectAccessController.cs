using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Features.ProjectAccess.Services;
// using PlcBase.Features.ProjectAccess.DTOs;
using PlcBase.Shared.Utilities;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Controller;
using PlcBase.Base.DTO;

namespace PlcBase.Features.ProjectAccess.Controllers;

public class ProjectAccessController : BaseController
{
    private readonly IProjectAccessService _projectAccessService;

    public ProjectAccessController(IProjectAccessService projectAccessService)
    {
        _projectAccessService = projectAccessService;
    }
}
