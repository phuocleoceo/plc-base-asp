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

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(int id);

    Task RemoveAsync(T entity);

    Task RemoveRangeAsync(IEnumerable<T> entity);

    Task<int> Save();
}