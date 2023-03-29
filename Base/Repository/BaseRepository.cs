using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcBase.Base.DomainModel;
using System.Linq.Expressions;
using PlcBase.Models.Context;
using AutoMapper;

namespace PlcBase.Base.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    internal DbSet<T> _dbSet;

    public BaseRepository(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _dbSet = _db.Set<T>();
    }

    public async Task<List<U>> GetAllAsync<U>(QueryModel<T> queryModel = null)
    {
        IQueryable<T> query = queryModel != null ? GetQuery(queryModel) : _dbSet;
        return await query.ProjectTo<U>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<U> GetOneAsync<U>(QueryModel<T> queryModel = null)
    {
        IQueryable<T> query = queryModel != null ? GetQuery(queryModel) : _dbSet;
        return await query.ProjectTo<U>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }

    protected IQueryable<T> GetQuery(QueryModel<T> queryModel)
    {
        IQueryable<T> query = _dbSet;

        if (!queryModel.Tracking)
        {
            query = query.AsNoTracking();
        }

        foreach (Expression<Func<T, object>> includeProp in queryModel.Includes)
        {
            query = query.Include(includeProp);
        }

        foreach (Expression<Func<T, bool>> filterCondition in queryModel.Filters)
        {
            query = query.Where(filterCondition);
        }

        if (queryModel.OrderBy != null)
        {
            query = queryModel.OrderBy(query);
        }

        return query;
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