using Domain.Shared;

namespace Domain.Errors
{
    public static class DomainError
    {
        public static class UserError {

            public static readonly Error EmailIsArleadyUsed = new Error
            (
                "User.Create",
                "The email is arleady used"
            );
        }
    }
}
