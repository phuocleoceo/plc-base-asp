using System.Linq.Expressions;

namespace PlcBase.Base.DomainModel;

public class QueryModel<T>
{
    public List<Expression<Func<T, bool>>> Filters { get; set; } = new();

    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public bool Tracking { get; set; } = false;
}
