using Domain.Primitives;

namespace Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entity);
    }
}
