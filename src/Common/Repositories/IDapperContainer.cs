using PlcBase.Base.DomainModel;

namespace PlcBase.Common.Repositories;

public interface IDapperContainer : IDisposable
{
    Task ExecuteAsync(DapperQuery dapperQuery);

    Task<List<TDto>> QueryAsync<TEntity, TDto>(DapperQuery dapperQuery);

    Task<Tuple<List<T1>, List<T2>>> QueryMultipleAsync<T1, T2>(DapperQuery dapperQuery);

    Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(DapperQuery dapperQuery);

    Task<TEntity> QuerySingleAsync<TEntity>(DapperQuery dapperQuery);

    Task<TEntity> ExecuteScalarAsync<TEntity>(DapperQuery dapperQuery);
}
