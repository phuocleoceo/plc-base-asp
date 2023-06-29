namespace PlcBase.Features.ProjectAccess.DTOs;

public class ProjectPermissionGroupDTO
{
    public string Module { get; set; }

    public IEnumerable<ProjectPermissionDTO> Children { get; set; }
}
