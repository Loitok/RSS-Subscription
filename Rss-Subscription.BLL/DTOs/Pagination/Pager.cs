using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rss_Subscription.BLL.Extensions;

namespace Rss_Subscription.BLL.DTOs.Pagination
{
    public class Pager<TData> where TData : class
    {
        public int CurrentItemNumber { get; }
        public int PageSize { get; }

        private readonly int _pageNumber;
        private readonly int _totalPages;
        private readonly int _totalCount;

        public Pager(int totalCount, int pageNumber, int pageSize)
        {
            _totalCount = totalCount;
            _pageNumber = pageNumber;
            CurrentItemNumber = (pageNumber - 1) * pageSize;
            _totalPages = (int)Math.Ceiling(((double)totalCount / pageSize));
            PageSize = pageSize;
        }

        public bool IsValidPage => _pageNumber > 0 && _pageNumber <= _totalPages;

        public Pagination<TData> GetEmptyPagination()
        {
            return new Pagination<TData>
            {
                PageSize = PageSize,
                TotalItems = 0,
                Items = Enumerable.Empty<TData>()
            };
        }

        public Pagination<TData> GetPagination(IList<TData> items)
        {
            return new Pagination<TData>
            {
                PageSize = PageSize,
                TotalItems = _totalCount,
                Items = items
            };
        }

        public Pagination<TData> GetPagination(IEnumerable<TData> items)
        {
            return new Pagination<TData>
            {
                PageSize = PageSize,
                TotalItems = _totalCount,
                Items = items
            };
        }

        public async Task<IEnumerable<T>> GetPaginatedItemsAsync<T>(IQueryable<T> items) where T : class
        {
            if (!IsValidPage)
                return Enumerable.Empty<T>();

            return await items.Page(CurrentItemNumber, PageSize).ToArrayAsync();
        }
    }
}
