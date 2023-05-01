using AutoMapper;

using PlcBase.Features.Media.Entities;
using PlcBase.Features.Media.DTOs;
using PlcBase.Common.Repositories;
using PlcBase.Base.DomainModel;
using PlcBase.Shared.Enums;

namespace PlcBase.Features.Media.Services;

public class MediaService : IMediaService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public MediaService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<bool> CreateMedia(CreateMediaDTO createMediaDTO)
    {
        MediaEntity mediaEntity = _mapper.Map<MediaEntity>(createMediaDTO);

        _uow.Media.Add(mediaEntity);
        return await _uow.Save();
    }

    public async Task<bool> CreateManyMedia(IEnumerable<CreateMediaDTO> createMediaDTOs)
    {
        IEnumerable<MediaEntity> mediaEntities = _mapper.Map<IEnumerable<MediaEntity>>(
            createMediaDTOs
        );

        _uow.Media.AddRange(mediaEntities);
        return await _uow.Save();
    }

    public async Task<List<MediaDTO>> GetAllMediaOfIssue(int issueId)
    {
        return await _uow.Media.GetManyAsync<MediaDTO>(
            new QueryModel<MediaEntity>()
            {
                Filters = { m => m.EntityId == issueId && m.EntityType == EntityType.ISSUE },
            }
        );
    }
}
