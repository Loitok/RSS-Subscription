using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Rss_Subscription.BLL.DTOs.Feed
{
    public class FeedDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnread { get; set; }
        public string Title { get; set; }
        public string SourceFeed { get; set; }
        public string Author { get; set; }

        public Uri BaseUri { get; set; }
        public Uri SourceImageUrl { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }
}
