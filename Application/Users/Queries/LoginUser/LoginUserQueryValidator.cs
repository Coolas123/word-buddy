using Application.HelpClasses;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;

namespace Application.Users.Queries.LoginUser
{
    public sealed class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        private User user;
        public LoginUserQueryValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Введите почту")
                .MustAsync(async (email, cancellation) =>
                {
                    user = await userRepository.GetByEmailAsync(email!);
                    if (user != null)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("Неверная почта")
                .MaximumLength(64)
                .WithMessage("Почта не должна превышать 64 символа");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Введите пароль")
                .Must(password =>
                {
                    if (user != null && user.HashPassword == HashPassword.Generate(password!))
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("Неверный пароль");
        }
    }
}
