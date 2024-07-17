using System.Data;
using Dapper;

namespace PlcBase.Base.DomainModel;

public class DapperQuery
{
    public string Query { get; set; } = null;

    public DynamicParameters Params { get; set; } = null;

    public IDbTransaction Transaction { get; set; } = null;

    public int? CommandTimeout { get; set; } = null;

    public CommandType CommandType { get; set; } = CommandType.Text;
}
