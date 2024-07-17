using System.Data;
using Dapper;

namespace PlcBase.Common.Repositories;

public interface IDapperContainer : IDisposable
{
    Task<T> Single<T>(DapperQuery dapperQuery);

    Task Execute(DapperQuery dapperQuery);

    Task<T> OneRecord<T>(DapperQuery dapperQuery);

    Task<IEnumerable<T>> List<T>(DapperQuery dapperQuery);

    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> List<T1, T2>(DapperQuery dapperQuery);
}

public class DapperQuery
{
    public string Query { get; set; } = null;

    public DynamicParameters Params { get; set; } = null;

    public IDbTransaction Transaction { get; set; } = null;

    public int? CommandTimeout { get; set; } = null;

    public CommandType CommandType { get; set; } = CommandType.Text;
}
