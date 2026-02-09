using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence
{
    //only used to evaluate the specifications and return the list of entities based on the specifications without the need to check anything in the repository layer
    //evaluator class is a class that takes the specifications and evaluates them and returns the list of entities based on the specifications
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntiy> CreateQuery<TEntiy, Tkey>
            (IQueryable<TEntiy> EntryPoint, ISpecifications<TEntiy, Tkey> specifications) where TEntiy : BaseEntity<Tkey>
        {
            var Query = EntryPoint; // byb3tt el context 3la el entry point w el entry point hya el db set bta3t el entity



            if (specifications is not null)
            {
                if (specifications.IncludeEcplressions is not null && specifications.IncludeEcplressions.Any())
                {
                    //    foreach (var includeExp in specifications.IncludeEcplressions)
                    //    {
                    //        Query = Query.Include(includeExp);
                    //    }

                                                                //acummlate  ============ el hadifo 
                    Query = specifications.IncludeEcplressions.Aggregate(Query, (current, include) => current.Include(include));

                    //context.Products.Include(p => p.Category).Include(p => p.Supplier)  ===>  context.Products.Include(p => p.Category).Include(p => p.Supplier)  ===>  context.Products.Include(p => p.Category).Include(p => p.Supplier)
                }
            } 







            return Query;
        }
    }
}
