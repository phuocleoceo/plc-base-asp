using PlcBase.Repositories.Interface;
using PlcBase.Base.Repository;
using PlcBase.Models.Entities;
using PlcBase.Models.Context;

namespace PlcBase.Repositories.Implement;

public class ExampleRepository : BaseRepository<ExampleEntity>, IExampleRepository
{
    private readonly DataContext _db;

    public ExampleRepository(DataContext db) : base(db)
    {
        _db = db;
    }
}