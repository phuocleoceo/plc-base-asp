using System.Linq.Expressions;

namespace PlcBase.Base.Repository;

public interface IBaseRepository<T> where T : class
{

    IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includes = null,
                           bool tracking = true);

    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                   string includes = null,
                                   bool tracking = true);

    Task<T> FindByIdAsync(int id);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task RemoveById(int id);
}