using FluentValidation;

namespace Application.Words.Commands.CreateWord
{
    public sealed class CreateWordsCommandValidator : AbstractValidator<CreateWordsCommand>
    {
        public CreateWordsCommandValidator() {
            RuleForEach(x => x.Words)
                .Cascade(CascadeMode.Stop)
                .ChildRules(w => {
                    w.RuleFor(x => x.Text)
                    .NotEmpty()
                    .WithMessage("Введите слово");
                })
            .OverridePropertyName("CreateDictionaryRowCommand.CreateWordCommand.CreateWords");
        }
    }
}
