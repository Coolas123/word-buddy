using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class GeneratedTextHistoryConfiguration : IEntityTypeConfiguration<GeneratedTextHistory>
    {
        public void Configure(EntityTypeBuilder<GeneratedTextHistory> builder) {
            builder
                .ToTable("generated_text_history")
                .HasKey(x=>x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.UserId)
                .HasColumnName("user_id");
            
            builder
                .Property(x => x.Text)
                .HasColumnName("text");
        }
    }
}
