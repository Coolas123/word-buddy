using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class DictionaryConfiguration : IEntityTypeConfiguration<Dictionary>
    {
        public void Configure(EntityTypeBuilder<Dictionary> builder) {
            builder
                .ToTable("dictionary")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder.
               Property(x => x.Title)
               .HasColumnName("title")
               .HasMaxLength(128);

            builder.
                Property(x=>x.Description)
                .HasColumnName("description")
                .HasMaxLength(256);

            builder.
                Property(x => x.WordLanguage)
                .HasColumnName("word_language");

            builder.
                Property(x => x.TranslationLanguage)
                .HasColumnName("translation_language");

            builder.
                Property(x => x.LastViewedAt)
                .HasColumnName("last_viewed_at");

            builder.
                Property(x => x.UserId)
                .HasColumnName("user_id");

            builder
                .HasMany(x => x.Words)
                .WithOne()
                .HasForeignKey(x => x.DictionaryId);
        }
    }
}
