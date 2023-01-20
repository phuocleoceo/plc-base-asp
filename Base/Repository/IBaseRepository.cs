using System.Linq.Expressions;

namespace PlcBase.Base.Repository;

public interface IBaseRepository<T> where T : class
{

    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     string includes = null,
                                     bool tracking = true);

    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                   string includes = null,
                                   bool tracking = true);

    Task<T> FindByIdAsync(int id);

    Task<bool> AddAsync(T entity);

    Task<bool> UpdateAsync(T entity);

    Task<bool> RemoveAsync(int id);

    Task<bool> RemoveAsync(T entity);

    Task<bool> RemoveRangeAsync(IEnumerable<T> entity);
}