using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.SharedLibirary
{

    public enum ProductSortOptions : byte
    {
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4
    }
    public class ProductQueryPrams
    {
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public int? BrandId { get; set; }

        public ProductSortOptions? Sort { get; set; }

        private int _PageIndex = 1;

        public int PageIndex {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = (value<=0)?1:value;
            }
        }

        private const int Default_PageSize = 10;
        private const int Max_PageSize = 50;

        private int _PageSize = Default_PageSize;

        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                if(value<=0)
                {
                    _PageSize = Default_PageSize;
                }
                else if(value>Max_PageSize)
                {
                    _PageSize = Max_PageSize;
                }
                else
                {
                    _PageSize=value;
                }
            }
        }



    }
}
