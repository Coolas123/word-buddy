using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public SystemRole SystemRole { get; private set; }
        public UserRole UserRole { get; private set; }
        public string HashPassword { get; private set; }

        public User(Guid id) : base(id) {
        }
    }
}
