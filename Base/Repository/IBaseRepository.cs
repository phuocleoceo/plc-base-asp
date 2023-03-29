using System.Linq.Expressions;
using PlcBase.Base.DomainModel;

namespace PlcBase.Base.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<List<U>> GetAllAsync<U>(QueryModel<T> queryModel = null);

    Task<U> GetOneAsync<U>(QueryModel<T> queryModel = null);

    Task<T> FindByIdAsync(int id);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task RemoveById(int id);
}