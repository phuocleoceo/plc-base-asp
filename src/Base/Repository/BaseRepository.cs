using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

using PlcBase.Common.Data.Context;
using PlcBase.Base.DomainModel;
using PlcBase.Base.Entity;
using PlcBase.Base.DTO;

namespace PlcBase.Base.Repository;

public class BaseRepository<T> : IBaseRepository<T>
    where T : BaseEntity
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;
    internal DbSet<T> _dbSet;

    protected BaseRepository(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
        _dbSet = _db.Set<T>();
    }

    public async Task<List<U>> GetManyAsync<U>(QueryModel<T> queryModel = null)
        where U : class
    {
        IQueryable<T> query = GetQueryable(queryModel);

        return typeof(U) != typeof(T)
            ? await query.ProjectTo<U>(_mapper.ConfigurationProvider).ToListAsync()
            : await query.ToListAsync() as List<U>;
    }

    public async Task<PagedList<U>> GetPagedAsync<U>(QueryModel<T> queryModel = null)
        where U : class
    {
        IQueryable<T> query = GetQueryable(queryModel);

        int count = query.Count();

        query = query
            .Skip((queryModel.PageNumber - 1) * queryModel.PageSize)
            .Take(queryModel.PageSize);

        List<U> items =
            typeof(U) != typeof(T)
                ? await query.ProjectTo<U>(_mapper.ConfigurationProvider).ToListAsync()
                : await query.ToListAsync() as List<U>;

        return new PagedList<U>(items, count);
    }

    public async Task<U> GetOneAsync<U>(QueryModel<T> queryModel = null)
        where U : class
    {
        IQueryable<T> query = GetQueryable(queryModel);

        return typeof(U) != typeof(T)
            ? await query.ProjectTo<U>(_mapper.ConfigurationProvider).FirstOrDefaultAsync()
            : await query.FirstOrDefaultAsync() as U;
    }

    private IQueryable<T> GetQueryable(QueryModel<T> queryModel)
    {
        IQueryable<T> query = _dbSet;

        if (queryModel == null)
            return query;

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

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
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

    public async Task RemoveRangeById(IEnumerable<int> ids)
    {
        IEnumerable<T> entities = await _dbSet
            .Where(entity => ids.Contains(entity.Id))
            .ToListAsync();

        RemoveRange(entities);
    }

    public void SoftDelete(T entity)
    {
        if (entity is ISoftDeletable softDeletableEntity)
        {
            softDeletableEntity.DeletedAt = DateTime.UtcNow;
            Update(softDeletableEntity as T);
        }
    }

    public async Task SoftDeleteById(int id)
    {
        T entity = await _dbSet.FindAsync(id);
        SoftDelete(entity);
    }

    public async Task SoftDeleteByIds(IEnumerable<int> ids)
    {
        await _dbSet
            .Where(entity => ids.Contains(entity.Id))
            .ForEachAsync(entity => SoftDelete(entity));
    }
}
