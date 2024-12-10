using Domain.Primitives;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity entity) {
            dbSet.Update(entity);
        }
    }
}
