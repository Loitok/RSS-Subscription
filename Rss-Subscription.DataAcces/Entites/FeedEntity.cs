using System;
using System.Collections.Generic;

namespace Rss_Subscription.DataAccess.Entites
{
    public class FeedEntity
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
