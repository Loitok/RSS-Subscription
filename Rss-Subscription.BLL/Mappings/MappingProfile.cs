using AutoMapper;
using Rss_Subscription.BLL.DTOs.Feed;
using Rss_Subscription.DataAccess.Entites;

namespace Rss_Subscription.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FeedEntity, FeedDto>();
        }
    }
}
