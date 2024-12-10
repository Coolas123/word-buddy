using FluentValidation;

namespace Application.Users.Commands.ChangeSettings
{
    internal class ChangeUserSettingsCommandValidator : AbstractValidator<ChangeUserSettingsCommand>
    {
        public ChangeUserSettingsCommandValidator() {
            RuleFor(x => x.UserName)
                .MaximumLength(64)
                .WithMessage("прозвище должно быть не длинее 64 символов")
                .Matches(@"[A-Za-z0-9]+")
                .WithMessage("Прозвище должно состоять из латинских букв, цифр");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Неверный формат почты")
                .MaximumLength(64)
                .WithMessage("Почта не должна превышать 64 символа");

            RuleFor(x => x.Password)
                .MaximumLength(32)
                .WithMessage("Пароль не должен превышать 32 символа")
                .MinimumLength(6)
                .WithMessage("Пароль должен содержать минимум 6 символов")
                .Matches(@"((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%!]).{6,20})")
                .WithMessage("Пароль должен содержать: Латинскую букву в нижнем и верхнем регистре, одна цифра, спецсимвол @#$%!");

            When(user => !string.IsNullOrEmpty(user.Password), () => {
                RuleFor(user => user.ConfirmPassword)
               .Equal(x => x.Password)
               .WithMessage("Пароли не совпадают")
               .NotEmpty()
               .WithMessage("Повторите пароль");
            });
        }
    }
}
