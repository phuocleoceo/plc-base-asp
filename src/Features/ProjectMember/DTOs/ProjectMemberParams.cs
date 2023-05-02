using PlcBase.Base.DomainModel;

namespace PlcBase.Features.ProjectMember.DTOs;

public class ProjectMemberParams : ReqParam
{
    public bool WithDeleted { get; set; } = false;
}
