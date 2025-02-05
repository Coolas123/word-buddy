using Domain.Repositories;
using FluentValidation;

namespace Application.Users.Commands.RegisterUser
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IUserRepository userRepository) {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Введите прозвище")
                .MaximumLength(64)
                .WithMessage("прозвище должно быть не длинее 64 символов")
                .Matches(@"[A-Za-z0-9]+")
                .WithMessage("Прозвище должно состоять из латинских букв, цифр");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Введите пароль")
                .MaximumLength(32)
                .WithMessage("Пароль не должен превышать 32 символа")
                .MinimumLength(6)
                .WithMessage("Пароль должен содержать минимум 6 символов");

            RuleFor(x => x.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Повторите пароль")
                .Equal(x => x.Password)
                .WithMessage("Пароли не совпадают");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Введите почту")
                .MustAsync(async (email, cancellation) => {
                    var user = await userRepository.GetByEmailAsync(email!);
                    if (user != null) {
                        return false;
                    }
                    return true;
                })
                .WithMessage("Пользователь с такой почтой уже существует")
                .EmailAddress()
                .WithMessage("Неверный формат почты")
                .MaximumLength(64)
                .WithMessage("Почта не должна превышать 64 символа");
        }
    }
}
