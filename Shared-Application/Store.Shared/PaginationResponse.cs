using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Shared
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; } // Current page index
        public int PageSize { get; set; } // Number of items per page
        public int Count { get; set; } // Total number of items
        public IEnumerable<TEntity> Data { get; set; } // Data for the current page
    }
}
