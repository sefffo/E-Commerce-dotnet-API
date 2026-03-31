using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Services.Specifications
{
    public abstract class BaseSpecification<TEntiy, TKey> : ISpecifications<TEntiy, TKey> where TEntiy : BaseEntity<TKey>
    {
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginationEnabled { get; private set; }

        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginationEnabled = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }

        public Expression<Func<TEntiy, bool>> Criteria { get; }

        protected BaseSpecification(Expression<Func<TEntiy, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public Expression<Func<TEntiy, object>> OrderBy { get; private set; }
        protected void OrderByExpression(Expression<Func<TEntiy, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public Expression<Func<TEntiy, object>> OrderByDescending { get; private set; }
        protected void OrderByDescindingExpression(Expression<Func<TEntiy, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        public ICollection<Expression<Func<TEntiy, object>>> IncludeExplressions { get; } = [];

        // String-based includes for ThenInclude support
        public ICollection<string> IncludeStrings { get; } = [];

        protected void AddInclude(Expression<Func<TEntiy, object>> includeExpression)
        {
            IncludeExplressions.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
