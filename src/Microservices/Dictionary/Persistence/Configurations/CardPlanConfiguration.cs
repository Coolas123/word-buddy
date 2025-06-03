using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class CardPlanConfiguration : IEntityTypeConfiguration<CardPlan>
    {
        public void Configure(EntityTypeBuilder<CardPlan> builder) {
            builder
                .ToTable("card_plan")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x=>x.UserId)
                .HasColumnName("user_id");

            builder.
               Property(x => x.Title)
               .HasColumnName("title")
               .HasMaxLength(128);

            builder.
                Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(256);

            builder.
                Property(x => x.ViewedAt)
                .HasColumnName("viewed_at");

            builder.HasMany(x => x.Dictionaries)
                .WithOne()
                .HasForeignKey(x=>x.CardPlanId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.CardBoxes)
                .WithOne()
                .HasForeignKey(x=>x.CardPlanId);
        }
    }
}
