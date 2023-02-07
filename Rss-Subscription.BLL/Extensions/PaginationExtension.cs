using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rss_Subscription.BLL.DTOs.Pagination;
using Rss_Subscription.BLL.DTOs.Pagination.Abstractions;

namespace Rss_Subscription.BLL.Extensions
{
    public static class PaginationExtension
    {
        public static Task<IPagination<TResult>> GetPaginationWithMapAsync<TResult>(this IQueryable<object> items, PaginationDto pagination, IMapper mapper) where TResult : class
            => GetPaginationWithMapAsync<TResult>(items, pagination?.PageNumber ?? PaginationDto.Default.PageNumber, pagination?.PageSize ?? PaginationDto.Default.PageSize, mapper);

        public static async Task<IPagination<TResult>> GetPaginationWithMapAsync<TResult>(this IQueryable<object> items, int pageNumber, int pageSize, IMapper mapper) where TResult : class
        {
            var pager = await items.GetPagerAsync<TResult>(pageNumber, pageSize);

            if (!pager.IsValidPage)
                return pager.GetEmptyPagination();

            var result = await items.Page(pager.CurrentItemNumber, pager.PageSize).ToArrayAsync();

            return result.GetType() != typeof(TResult)
                ? mapper != null
                    ? pager.GetPagination(mapper.Map<IEnumerable<TResult>>(result))
                    : null
                : pager.GetPagination(result as IEnumerable<TResult>);
        }

        public static async Task<Pager<T>> GetPagerAsync<T>(this IQueryable<object> items, int pageNumber, int pageSize) where T : class
            => new Pager<T>(await items.CountAsync(), pageNumber, pageSize);

        public static IQueryable<T> Page<T>(this IQueryable<T> items, int skip, int take)
            => items.Skip(skip).Take(take);
    }
}
