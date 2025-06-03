using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opt): base(opt) {
            Database.EnsureCreated();
        }

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<DictionaryRow> Words { get; set; }
        public DbSet<CardBox> CardBox { get; set; }
        public DbSet<CardPlan> CardPlan { get; set; }
        public DbSet<GeneratedTextHistory> GeneratedTextHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
