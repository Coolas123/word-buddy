using Domain.EntityServices;
using Domain.Repositories;

namespace Application.DomainServices
{
    public sealed class EmailUniqueCheck : IEmailUniqueCheck
    {
        private readonly IUserRepository userRepository;
        public EmailUniqueCheck(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }
        public async Task<bool> IsUnique(string email) {
            var dbEmail = await userRepository.GetByEmailAsync(email);
            return dbEmail == null;
        }
    }
}
