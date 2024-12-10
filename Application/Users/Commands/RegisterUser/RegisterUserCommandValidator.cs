using FluentValidation;

namespace Application.Users.Commands.RegisterUser
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Введите прозвище")
                .MaximumLength(64)
                .WithMessage("прозвище должно быть не длинее 64 символов")
                .Matches(@"[A-Za-z0-9]+")
                .WithMessage("Прозвище должно состоять из латинских букв, цифр");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Введите пароль")
                .MaximumLength(32)
                .WithMessage("Пароль не должен превышать 32 символа")
                .MinimumLength(6)
                .WithMessage("Пароль должен содержать минимум 6 символов")
                .Matches(@"((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%!]).{6,20})")
                .WithMessage("Пароль должен содержать: Латинскую букву в нижнем и верхнем регистре, одна цифра, спецсимвол @#$%!");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Повторите пароль")
                .Equal(x => x.Password)
                .WithMessage("Пароли не совпадают");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Введите почту")
                .EmailAddress()
                .WithMessage("Неверный формат почты")
                .MaximumLength(64)
                .WithMessage("Почта не должна превышать 64 символа");
        }
    }
}
