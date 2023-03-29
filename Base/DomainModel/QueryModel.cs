using System.Linq.Expressions;

namespace PlcBase.Base.DomainModel;

public class QueryModel<T>
{
    public List<Expression<Func<T, bool>>> Filters { get; set; } = new List<Expression<Func<T, bool>>>();

    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

    public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; } = null;

    public int PageNumber { get; set; } = 0;

    public int PageSize { get; set; } = 0;

    public bool Tracking { get; set; } = false;
}