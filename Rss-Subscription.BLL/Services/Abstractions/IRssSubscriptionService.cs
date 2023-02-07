using Rss_Subscription.BLL.DTOs;
using Rss_Subscription.BLL.DTOs.Feed;
using Rss_Subscription.BLL.DTOs.Pagination;
using Rss_Subscription.BLL.DTOs.Pagination.Abstractions;
using Rss_Subscription.BLL.DTOs.Result.Abstractions;
using Rss_Subscription.BLL.DTOs.Result.Abstractions.Generics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rss_Subscription.BLL.Services.Abstractions
{
    public interface IRssSubscriptionService
    {
        Task<IResult<CreateResponseDto>> CreateFeedAsync(Uri feedUrl);
        Task<IResult<IPagination<FeedDto>>> GetActiveFeedsAsync(PaginationDto pagination);
        Task<IResult<IPagination<FeedDto>>> GetUnreadFeedsAsync(PaginationDto pagination, DateTime date);
        Task<IResult> SetAsReadAsync(IReadOnlyCollection<int> feedIds);
    }
}
