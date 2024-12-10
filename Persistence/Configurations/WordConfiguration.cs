using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder) {
            builder
                .ToTable("word")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Text)
                .HasColumnName("text");

            builder
                .Property(x => x.LearnStatus)
                .HasColumnName("learn_status");

            builder
                .Property(x => x.LearnStatusChangedAt)
                .HasColumnName("learn_status_changed_at");

            builder
                .HasOne(x => x.Translation)
                .WithOne()
                .HasForeignKey<Translation>(x=>x.WordId);
        }
    }
}
