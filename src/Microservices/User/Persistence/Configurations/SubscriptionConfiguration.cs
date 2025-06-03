using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder) {
            builder
                .ToTable("subscription")
                .HasKey(x=>x.UserId);

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_Id");

            builder
                .Property(x => x.SubscribedAt)
                .HasColumnName("subscribed_at");

            builder
                .Property(x => x.SubscriptionType)
                .HasColumnName("subscription_type");

            builder.Ignore(x => x.Id);
        }
    }
}
