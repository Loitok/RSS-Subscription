using System.Collections.Generic;

namespace Rss_Subscription.BLL.DTOs.Pagination.Abstractions
{
    public interface IPagination<TData> where TData : class
    {
        int PageSize { get; set; }
        int TotalItems { get; set; }
        IEnumerable<TData> Items { get; set; }
    }
}
