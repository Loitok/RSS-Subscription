using Microsoft.Extensions.Logging;
using Rss_Subscription.Api.Extensions;
using Rss_Subscription.BLL.DTOs.Pagination;
using Rss_Subscription.BLL.DTOs.User;
using Rss_Subscription.BLL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rss_Subscription.Api.Controllers
{
    [RoutePrefix("[controller]")]
    public class RssSubscriptionController : ApiController
    {
        private readonly ILogger<RssSubscriptionController> _logger;
        private readonly IRssSubscriptionService _rssSubscriptionService;
        public RssSubscriptionController(ILogger<RssSubscriptionController> logger, IRssSubscriptionService rssSubscriptionService)
        {
            _logger = logger;
            _rssSubscriptionService = rssSubscriptionService;
        }

        [HttpPost]
        [Route("feed")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IHttpActionResult> CreateFeed(Uri url)
            => (await _rssSubscriptionService.CreateFeedAsync(url)).ToActionResult(this);

        [HttpGet]
        [Route("feeds/active")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IHttpActionResult> GetActiveFeeds([FromUri] PaginationDto pagination)
            => (await _rssSubscriptionService.GetActiveFeedsAsync(pagination)).ToActionResult(this);

        [HttpGet]
        [Route("feeds/unread")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IHttpActionResult> GetUnreadFeeds([FromUri] PaginationDto pagination, DateTime date)
            => (await _rssSubscriptionService.GetUnreadFeedsAsync(pagination, date)).ToActionResult(this);

        [HttpPatch]
        [Route("feed/set-as-read")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IHttpActionResult> SetFeedAsRead([FromUri] IReadOnlyCollection<int> feedIds)
            => (await _rssSubscriptionService.SetAsReadAsync(feedIds)).ToActionResult(this);


    }
}
