using ECommerce.Domain.Entities.ProductModule;
using ECommerce.SharedLibirary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {



        //adding order specification 















        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }


        //get all products with their brands and types
        //adding filter to get a specific product with its brand and type by name 

        public ProductWithBrandAndTypeSpecification(ProductQueryPrams queryPrams) :
            base(
                //bid is null 
                //tid is null
                //both are not null
                p => (!queryPrams.BrandId.HasValue || p.BrandId == queryPrams.BrandId.Value) &&
                    (!queryPrams.TypeId.HasValue || p.TypeId == queryPrams.TypeId.Value) &&
                    (string.IsNullOrEmpty(queryPrams.Search) || p.Name.ToLower().Contains(queryPrams.Search.ToLower()))
            //uses like in the database to search for the product by name and ignore case sensitivity


            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryPrams.Sort)
            {
                case ProductSortOptions.NameAsc:
                    OrderByExpression(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    OrderByDescindingExpression(p => p.Name);
                    break;
                case ProductSortOptions.PriceAsc:
                    OrderByExpression(p => p.Price);
                    break;
                case ProductSortOptions.PriceDesc:
                    OrderByDescindingExpression(p => p.Price);
                    break;
                default:
                    OrderByExpression(p => p.Id);
                    break;

            }
        }
    }
}
