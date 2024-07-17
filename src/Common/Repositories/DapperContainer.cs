using Microsoft.EntityFrameworkCore;
using System.Data;
using AutoMapper;
using Dapper;

using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;

namespace PlcBase.Common.Repositories;

public class DapperContainer : IDapperContainer
{
    private readonly IMapper _mapper;
    private readonly DataContext _db;
    private readonly IDbConnection _connection;

    public DapperContainer(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _connection = _db.Database.GetDbConnection();
    }

    public void Dispose()
    {
        _db.Dispose();
        _connection.Dispose();
    }

    public async Task ExecuteAsync(DapperQuery dapperQuery)
    {
        await _connection.ExecuteAsync(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }

    public async Task<List<TDto>> QueryAsync<TEntity, TDto>(DapperQuery dapperQuery)
    {
        IEnumerable<TEntity> result = await _connection.QueryAsync<TEntity>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );

        return typeof(TDto) != typeof(TEntity)
            ? _mapper.Map<List<TDto>>(result)
            : result.ToList() as List<TDto>;
    }

    public async Task<Tuple<List<T1>, List<T2>>> QueryMultipleAsync<T1, T2>(DapperQuery dapperQuery)
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
            return new Tuple<List<T1>, List<T2>>(item1.ToList(), item2.ToList());
        }

        return new Tuple<List<T1>, List<T2>>(new List<T1>(), new List<T2>());
    }

    public async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(DapperQuery dapperQuery)
    {
        return await _connection.QueryFirstOrDefaultAsync<TEntity>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }

    public async Task<TEntity> QuerySingleAsync<TEntity>(DapperQuery dapperQuery)
    {
        return await _connection.QuerySingleAsync<TEntity>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }

    public async Task<TEntity> ExecuteScalarAsync<TEntity>(DapperQuery dapperQuery)
    {
        return await _connection.ExecuteScalarAsync<TEntity>(
            sql: dapperQuery.Query,
            param: dapperQuery.Params,
            transaction: dapperQuery.Transaction,
            commandTimeout: dapperQuery.CommandTimeout,
            commandType: dapperQuery.CommandType
        );
    }
}
