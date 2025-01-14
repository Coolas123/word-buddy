using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<User> GetByEmailAsync(string email) {
            return await dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
