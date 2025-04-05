using FluentValidation;

namespace Application.DictionaryRow.Commands.CreateWord
{
    public sealed class CreateDictionaryRowsCommandValidator : AbstractValidator<CreateDictionaryRowsCommand>
    {
        public CreateDictionaryRowsCommandValidator() {
            RuleForEach(x => x.DictionaryRows)
                .Cascade(CascadeMode.Stop)
                .ChildRules(w => {
                    w.RuleFor(x => x.WordText)
                    .NotEmpty()
                    .WithMessage("Введите слово");
                })
                .ChildRules(w => {
                    w.RuleFor(x => x.WordTranslation)
                    .NotEmpty()
                    .WithMessage("Введите перевод");
                });
        }
    }
}
