using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.SharedLibirary
{

    public enum ProductSortOptions :byte
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc =3 ,
        PriceDesc = 4
    }
    public class ProductQueryPrams
    {
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public int? BrandId { get; set; }

        public ProductSortOptions? Sort { get; set; }
    }
}
