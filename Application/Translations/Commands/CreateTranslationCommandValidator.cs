using FluentValidation;

namespace Application.Translations.Commands
{
    public sealed class CreateTranslationCommandValidator : AbstractValidator<CreateTranslationCommand>
    {
        public CreateTranslationCommandValidator() {
            RuleForEach(x => x.CreateTranslations)
                .Cascade(CascadeMode.Stop)
                .ChildRules(w => {
                    w.RuleFor(x => x.Text)
                    .NotEmpty()
                    .WithMessage("Введите перевод");
                })
                .OverridePropertyName("CreateDictionaryRowCommand.CreateTranslationCommand.CreateTranslations");
        }
    }
}
