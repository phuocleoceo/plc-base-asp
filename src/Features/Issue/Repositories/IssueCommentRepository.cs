using AutoMapper;

using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Repository;

namespace PlcBase.Features.Issue.Repositories;

public class IssueCommentRepository : BaseRepository<IssueCommentEntity>, IIssueCommentRepository
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public IssueCommentRepository(DataContext db, IMapper mapper)
        : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IssueCommentEntity> GetForUpdateAndDelete(int userId, int issueId)
    {
        return await GetOneAsync<IssueCommentEntity>(
            new QueryModel<IssueCommentEntity>()
            {
                Filters = { i => i.Id == issueId && i.UserId == userId },
            }
        );
    }
}
