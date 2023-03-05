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

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
        {
            foreach (string includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query;
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null,
                                                string includes = null,
                                                bool tracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
        {
            foreach (string includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await Save() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
        return await Save() > 0;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        T entity = await _dbSet.FindAsync(id);
        await RemoveAsync(entity);
        return await Save() > 0;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        if (_db.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        return await Save() > 0;
    }

    public async Task<bool> RemoveRangeAsync(IEnumerable<T> entity)
    {
        _dbSet.RemoveRange(entity);
        return await Save() > 0;
    }

    private async Task<int> Save()
    {
        return await _db.SaveChangesAsync();
    }
}