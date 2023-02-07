using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rss_Subscription.BLL.DTOs;
using Rss_Subscription.BLL.DTOs.Feed;
using Rss_Subscription.BLL.DTOs.Pagination;
using Rss_Subscription.BLL.DTOs.Pagination.Abstractions;
using Rss_Subscription.BLL.DTOs.Result.Abstractions;
using Rss_Subscription.BLL.DTOs.Result.Abstractions.Generics;
using Rss_Subscription.BLL.DTOs.Result.Implementations;
using Rss_Subscription.BLL.DTOs.Result.Implementations.Generics;
using Rss_Subscription.BLL.Extensions;
using Rss_Subscription.BLL.Services.Abstractions;
using Rss_Subscription.BLL.Texts;
using Rss_Subscription.DataAccess.Abstractions;
using Rss_Subscription.DataAccess.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Rss_Subscription.BLL.Services.Implementations
{
    public class RssSubscriptionService : IRssSubscriptionService
    {

        private readonly IRepository<FeedEntity> _feedRepository;
        private readonly IMapper _mapper;

        public RssSubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _feedRepository = unitOfWork.Repository<FeedEntity>();
            _mapper = mapper;
        }

        public async Task<IResult<CreateResponseDto>> CreateFeedAsync(Uri feedUrl)
        {
            try
            {
                var reader = XmlReader.Create(feedUrl.AbsoluteUri);
                var feed = SyndicationFeed.Load(reader);
                reader.Close();

                var feedIds = new List<int>();

                foreach (var item in feed.Items)
                {
                    var entity = new FeedEntity
                    {
                        Author = item.Authors
                            .Select(x => x.Name)
                            .FirstOrDefault(),
                        PublishDate = item.PublishDate,
                        Title = item.Title.Text,
                        SourceFeed = item.SourceFeed.Title.Text,
                        IsActive = item.PublishDate >= DateTimeOffset.Now - TimeSpan.FromDays(7), 
                        IsUnread = true,
                        BaseUri = item.BaseUri,
                        SourceImageUrl = item.SourceFeed.ImageUrl
                    };
                    
                    await _feedRepository.InsertAsync(entity);

                    feedIds.Add(entity.Id);
                }

                return Result<CreateResponseDto>.CreateSuccess(new CreateResponseDto(feedIds));
            }
            catch (Exception e)
            {
                return Result<CreateResponseDto>.CreateFailure(FeedText.Error.CreateFeedError, e);
            }
        }

        public async Task<IResult<IPagination<FeedDto>>> GetActiveFeedsAsync(PaginationDto pagination)
        {
            try
            {
                var feedsQuery = _feedRepository.All
                    .AsNoTracking()
                    .Where(x => x.IsActive);

                var result = await feedsQuery.GetPaginationWithMapAsync<FeedDto>(pagination, _mapper);

                return Result<IPagination<FeedDto>>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<IPagination<FeedDto>>.CreateFailure(FeedText.Error.GetFeedsError, e);
            }
        }

        public async Task<IResult<IPagination<FeedDto>>> GetUnreadFeedsAsync(PaginationDto pagination, DateTime date)
        {
            try
            {
                var feedsQuery = _feedRepository.All
                    .AsNoTracking()
                    .Where(x => x.IsUnread && x.PublishDate > date);

                var result = await feedsQuery.GetPaginationWithMapAsync<FeedDto>(pagination, _mapper);

                return Result<IPagination<FeedDto>>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<IPagination<FeedDto>>.CreateFailure(FeedText.Error.GetFeedsError, e); 
            }
        }

        public async Task<IResult> SetAsReadAsync(IReadOnlyCollection<int> feedIds)
        {
            try
            {
                await _feedRepository.All
                    .Where(x => feedIds.Contains(x.Id))
                    .UpdateFromQueryAsync(x => new FeedEntity { IsUnread = false });

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure(FeedText.Error.SetAsUnreadFeedsError, e);
            }
        }
    }
}
