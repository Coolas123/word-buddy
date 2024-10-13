using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opt): base(opt) {
            Database.EnsureCreated();
        }

        //DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
