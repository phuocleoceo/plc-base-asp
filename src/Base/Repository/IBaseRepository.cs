using System.Linq.Expressions;

using PlcBase.Base.DomainModel;
using PlcBase.Base.Entity;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Repository;

public interface IBaseRepository<T>
    where T : BaseEntity
{
    Task<List<U>> GetManyAsync<U>(QueryModel<T> queryModel = null)
        where U : class;

    Task<PagedList<U>> GetPagedAsync<U>(QueryModel<T> queryModel = null)
        where U : class;

    Task<U> GetOneAsync<U>(QueryModel<T> queryModel = null)
        where U : class;

    Task<T> FindByIdAsync(int id);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task RemoveById(int id);

    Task RemoveRangeById(IEnumerable<int> ids);

    void SoftDelete(T entity);

    Task SoftDeleteById(int id);

    Task SoftDeleteByIds(IEnumerable<int> ids);
}
