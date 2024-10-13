using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder
                .ToTable("user")
                .HasKey(x=>x.Id);

            builder
                .Property(x=>x.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(64);

            builder
                .Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(64);

            builder
                .Property(x => x.SystemRole)
                .HasColumnName("system_role");

            builder
                .Property(x => x.UserRole)
                .HasColumnName("user_role");

            builder
                .Property(x => x.HashPassword)
                .HasColumnName("hash_password");
        }
    }
}
