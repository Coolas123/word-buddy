using FluentValidation;

namespace Application.Dictionaries.Commands.CreateDictionary
{
    internal sealed class CreateDictionaryCommandValidator : AbstractValidator<CreateDictionaryCommand>
    {
        public CreateDictionaryCommandValidator() {
            RuleFor(x => x.Title)
                .MinimumLength(1)
                .WithMessage("Минимальная длина заголовка должна быть 1 символ")
                .MaximumLength(128)
                .WithMessage("Максимальная длина заголовка должна быть 128 символ")
                .NotEmpty()
                .WithMessage("Введите название словаря");

            RuleFor(x => x.Description)
                .MaximumLength(256)
                .WithMessage("Длина описания не должна превышать 256 символов");

            RuleFor(x => x.WordLanguage)
                .NotEmpty()
                .WithMessage("Выберите язык слова");

            RuleFor(x => x.TranslationLanguage)
                .NotEmpty()
                .WithMessage("Выберите язык перевода");
        }
    }
}
