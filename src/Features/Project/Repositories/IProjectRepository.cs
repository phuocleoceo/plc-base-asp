using PlcBase.Features.Project.Entities;
using PlcBase.Base.Repository;
using PlcBase.Base.DomainModel;

namespace PlcBase.Features.Project.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task<ProjectEntity> GetByIdAndOwner(ReqUser reqUser, int projectId);
}
