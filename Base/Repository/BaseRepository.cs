using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PlcBase.Models.Context;

namespace PlcBase.Base.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DataContext _db;
    internal DbSet<T> _dbSet;
    public BaseRepository(DataContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }

    public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                 string includes = null,
                                                 bool tracking = true)
    {
        await Task.CompletedTask;
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            foreach (var includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                                string includes = null,
                                                bool tracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            foreach (var includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await Task.CompletedTask;
        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    public async Task RemoveAsync(int id)
    {
        T entity = await _dbSet.FindAsync(id);
        await RemoveAsync(entity);
    }

    public async Task RemoveAsync(T entity)
    {
        await Task.CompletedTask;
        if (_db.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entity)
    {
        await Task.CompletedTask;
        _dbSet.RemoveRange(entity);
    }
}