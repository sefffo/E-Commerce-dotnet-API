using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntiy> CreateQuery<TEntiy, Tkey>
            (IQueryable<TEntiy> EntryPoint, ISpecifications<TEntiy, Tkey> specifications) where TEntiy : BaseEntity<Tkey>
        {
            var Query = EntryPoint;
            if (specifications is not null)
            {
                if (specifications.OrderBy is not null)
                    Query = Query.OrderBy(specifications.OrderBy);

                if (specifications.OrderByDescending is not null)
                    Query = Query.OrderByDescending(specifications.OrderByDescending);

                if (specifications.Criteria is not null)
                    Query = Query.Where(specifications.Criteria);

                if (specifications.IsPaginationEnabled)
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);

                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                    Query = specifications.IncludeExpressions.Aggregate(Query, (current, include) => current.Include(include));

                // String-based includes for ThenInclude support (e.g. "Items.Product")
                if (specifications.IncludeStrings is not null && specifications.IncludeStrings.Any())
                    foreach (var include in specifications.IncludeStrings)
                        Query = Query.Include(include);
            }
            return Query;
        }
    }
}
