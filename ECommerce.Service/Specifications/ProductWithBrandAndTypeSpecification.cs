using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecification() : base()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
