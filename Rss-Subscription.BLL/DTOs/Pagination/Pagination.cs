using Rss_Subscription.BLL.DTOs.Pagination.Abstractions;
using System.Collections.Generic;

namespace Rss_Subscription.BLL.DTOs.Pagination
{
    public class Pagination<TData> : IPagination<TData> where TData : class
    {
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<TData> Items { get; set; }
    }
}
