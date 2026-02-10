using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.SharedLibirary
{
    public class PaginatedResult <TEntity> 
    {

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }
        = 0;

        public int PageSize {  get; set; }

        public IEnumerable<TEntity> Data { get; set; }


        public  PaginatedResult( int pageIndex, int pageSize, int totalCount , IEnumerable<TEntity> data)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }


    }
}
