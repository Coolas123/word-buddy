using FluentValidation;

namespace Application.Users.Commands.ChangeSettings
{
    internal class ChangeUserSettingsCommandValidator : AbstractValidator<ChangeUserSettingsCommand>
    {
        public ChangeUserSettingsCommandValidator() {
            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(64)
                .WithMessage("прозвище должно быть не длинее 64 символов")
                .Matches(@"[A-Za-z0-9]+")
                .WithMessage("Прозвище должно состоять из латинских букв, цифр");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress()
                .WithMessage("Неверный формат почты")
                .MaximumLength(64)
                .WithMessage("Почта не должна превышать 64 символа");

            When(user => !string.IsNullOrWhiteSpace(user.Password), () => {
                RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("Пароль должен содержать минимум 6 символов")
                .MaximumLength(32)
                .WithMessage("Пароль не должен превышать 32 символа");
            });

            When(user => !string.IsNullOrEmpty(user.Password) || !string.IsNullOrEmpty(user.ConfirmPassword), () => {
                RuleFor(user => user.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
               .Equal(x => x.Password)
               .WithMessage("Пароли не совпадают")
               .NotEmpty()
               .WithMessage("Повторите пароль");
            });
        }
    }
}
