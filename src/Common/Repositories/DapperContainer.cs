using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;

using PlcBase.Common.Data.Context;

namespace PlcBase.Common.Repositories;

public class DapperContainer : IDapperContainer
{
    private readonly DataContext _db;
    private readonly IDbConnection _connection;

    public DapperContainer(DataContext db)
    {
        _db = db;
        _connection = _db.Database.GetDbConnection();
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public async Task Execute(DapperQuery dapperQuery)
    {
        await _connection.ExecuteAsync(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }

    public async Task<IEnumerable<T>> List<T>(DapperQuery dapperQuery)
    {
        return await _connection.QueryAsync<T>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }

    public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> List<T1, T2>(DapperQuery dapperQuery)
    {
        SqlMapper.GridReader result = await _connection.QueryMultipleAsync(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );

        IEnumerable<T1> item1 = await result.ReadAsync<T1>();
        IEnumerable<T2> item2 = await result.ReadAsync<T2>();

        if (item1 != null && item2 != null)
        {
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(item1, item2);
        }

        return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
    }

    public async Task<T> OneRecord<T>(DapperQuery dapperQuery)
    {
        T value = await _connection.QueryFirstAsync<T>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
        return (T)Convert.ChangeType(value, typeof(T));
    }

    public async Task<T> Single<T>(DapperQuery dapperQuery)
    {
        T value = await _connection.ExecuteScalarAsync<T>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
        return (T)Convert.ChangeType(value, typeof(T));
    }
}
