using ECommerce.Domain.Entities.ProductModule;
using ECommerce.SharedLibirary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications
{
    internal class ProductCountSpecifications : BaseSpecification<Product, int>
    {
        public ProductCountSpecifications(ProductQueryPrams queryPrams) : base(p =>
        (string.IsNullOrEmpty(queryPrams.Search) || p.Name.ToLower().Contains(queryPrams.Search)) &&
        (!queryPrams.BrandId.HasValue || p.BrandId == queryPrams.BrandId) &&
        (!queryPrams.TypeId.HasValue || p.TypeId == queryPrams.TypeId))
        {
        }
    }
}
