using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder) {
            builder
                .ToTable("translation")
                .HasKey(x => x.WordId);

            builder.Ignore(x => x.Id);

            builder
                .Property(x=>x.Text)
                .HasColumnName("text");

            builder
                .Property(x=>x.WordId)
                .HasColumnName("word");
        }
    }
}
