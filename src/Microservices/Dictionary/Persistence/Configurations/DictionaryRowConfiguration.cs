using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class DictionaryRowConfiguration : IEntityTypeConfiguration<DictionaryRow>
    {
        public void Configure(EntityTypeBuilder<DictionaryRow> builder) {
            builder
                .ToTable("word")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.WordText)
                .HasMaxLength(128)
                .HasColumnName("word_text");

            builder
                .Property(x => x.WordTranslation)
                .HasMaxLength(256)
                .HasColumnName("word_translation");

            builder
                .Property(x => x.LearnStatus)
                .HasColumnName("learn_status");

            builder
                .Property(x => x.LearnStatusChangedAt)
                .HasColumnName("learn_status_changed_at");

            builder
                .Property(x => x.WordContexts)
                .HasColumnName("word_contexts");

            builder
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
