using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int) Math.Ceiling((decimal) TotalItems/PageSize);
    }

    public class IndexViewModel<T>
    {
        public IEnumerable<T> Entities { get; set; } 
        public PageInfo PageInfo { get; set; }
    }
}