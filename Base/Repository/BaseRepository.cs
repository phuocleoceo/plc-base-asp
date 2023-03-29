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

    public IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
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

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        if (_db.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task RemoveById(int id)
    {
        T entity = await _dbSet.FindAsync(id);
        Remove(entity);
    }
}