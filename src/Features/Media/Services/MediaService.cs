using PlcBase.Common.Repositories;

namespace PlcBase.Features.Media.Services;

public class MediaService : IMediaService
{
    private readonly IUnitOfWork _uow;

    public MediaService(IUnitOfWork uow)
    {
        _uow = uow;
    }
}
