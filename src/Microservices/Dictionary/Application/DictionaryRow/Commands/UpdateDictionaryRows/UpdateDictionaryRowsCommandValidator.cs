using FluentValidation;

namespace Application.DictionaryRow.Commands.UpdateWord.UpdateWords
{
    internal class UpdateDictionaryRowsCommandValidator : AbstractValidator<UpdateDictionaryRowsCommand>
    {
        public UpdateDictionaryRowsCommandValidator()
        {
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
