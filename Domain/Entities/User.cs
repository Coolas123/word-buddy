using Domain.EntityServices;
using Domain.Enums;
using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System.Security.Claims;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public Country Country { get; private set; }
        public SystemRole SystemRole { get; private set; }
        public UserRole UserRole { get; private set; }
        public string HashPassword { get; private set; }
        public IReadOnlyCollection<Dictionary> Dictionaries => _dictionaries;
        private List<Dictionary> _dictionaries = new();

        private User(Guid id,
            string userName,
            string email,
            Country country,
            string hashPassword,
            SystemRole systemRole,
            UserRole userRole)
            : base(id) {
            UserName = userName;
            Email = email;
            Country = country;
            HashPassword = hashPassword;
            SystemRole = systemRole;
            UserRole = userRole;
        }
        
        public async static Task<Result<User>> CreateAsync(
            Guid id,
            string userName,
            string email,
            Country country,
            string hashPassword,
            SystemRole systemRole,
            UserRole userRole,
            IEmailUniqueCheck emailUniqueCheck) {

            if (!await emailUniqueCheck.IsUnique(email)) {
                return Result.Failure<User>(DomainError.UserError.EmailIsArleadyUsed);
            }
            
            return Result.Success(
                    new User
                    (id,
                    userName,
                    email,
                    country,
                    hashPassword,
                    systemRole,
                    userRole));
        }

        public ClaimsIdentity GetClaims() {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,Email),
                new Claim("Id",Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((SystemRole)Enum
                .GetValues(typeof(SystemRole))
                .GetValue((int)SystemRole-1))
                .ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((UserRole)Enum
                .GetValues(typeof(UserRole))
                .GetValue((int)UserRole-1))
                .ToString())
            };

            return new ClaimsIdentity(claims);
        }

        public bool ChangeSettings(
            string? userName,
            string? email,
            Country? country,
            string hashPassword
            ) {
            bool isClaimIdentitiesChanged = false;

            if (userName != null && userName != UserName) {
                UserName = userName;
            }
            if (email != null && email != Email) {
                Email = email;
                isClaimIdentitiesChanged = true;
            }
            if (country != null && country != Country) {
                Country = (Country)country;
            }
            if (hashPassword != null && hashPassword != HashPassword) {
                HashPassword = hashPassword;
                isClaimIdentitiesChanged = true;
            }

            return isClaimIdentitiesChanged;
        }
    }
}
