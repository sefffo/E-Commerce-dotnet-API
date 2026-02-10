using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Services.Specifications
{


    // we need to have a partial implementation so we created base specification 
    //why its in the service ?? => because its related to the business logic and we need to use it in the service layer 


    //we se it as abstract class because we want to inherit from it and we dont want to create an instance from it  
    internal abstract class BaseSpecification<TEntiy, TKey> : ISpecifications<TEntiy, TKey> where TEntiy : BaseEntity<TKey>
    {




        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginationEnabled { get; private set; }

        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginationEnabled = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;//there is a fourmula to calculate the skip value => (PageIndex - 1) * PageSize

        }





        public Expression<Func<TEntiy, bool>> Criteria { get; } // we can add criteria to the specification to filter the data



        protected BaseSpecification(Expression<Func<TEntiy, bool>> criteriaExpression)
        //to make it mandatory to pass the criteria expression when we create a specification
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



        // we can add more common properties for all specifications like order by , pagination , etc

        // we can add a method to add include expressions to the collection
        protected void AddInclude(Expression<Func<TEntiy, object>> includeExpression) // we use protected because we want to use it in the derived classes and we dont want to use it outside the class
        {
            // we add the include expression to the collection
            IncludeExplressions.Add(includeExpression);
        }


    }
}
