using AutoMapper;

using PlcBase.Features.Issue.Entities;
using PlcBase.Common.Data.Context;
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
}
