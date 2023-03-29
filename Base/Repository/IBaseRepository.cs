using PlcBase.Base.DomainModel;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<List<U>> GetManyAsync<U>(QueryModel<T> queryModel = null);

    Task<PagedList<U>> GetPagedAsync<U>(QueryModel<T> queryModel = null);

    Task<U> GetOneAsync<U>(QueryModel<T> queryModel = null);

    Task<T> FindByIdAsync(int id);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task RemoveById(int id);
}