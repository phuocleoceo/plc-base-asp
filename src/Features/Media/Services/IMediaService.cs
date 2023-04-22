using PlcBase.Features.Media.DTOs;

namespace PlcBase.Features.Media.Services;

public interface IMediaService
{
    Task<bool> CreateMedia(CreateMediaDTO createMediaDTO);

    Task<bool> CreateManyMedia(IEnumerable<CreateMediaDTO> createMediaDTOs);

    Task<List<MediaDTO>> GetAllMediaOfIssue(int issueId);
}
