using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rss_Subscription.DataAccess.Entites;

namespace Rss_Subscription.DataAccess.Configurations
{
    public class FeedConfiguration : IEntityTypeConfiguration<FeedEntity>
    {
        public void Configure(EntityTypeBuilder<FeedEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.PublishDate)
                .IsRequired();

            builder.Property(x => x.BaseUri)
                .IsRequired();
        }
    }
}
