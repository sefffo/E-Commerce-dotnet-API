using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Domain.Interfaces
{
    public interface ISpecifications<TEntity, TKey>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        ICollection<string> IncludeStrings { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginationEnabled { get; }
    }
}
