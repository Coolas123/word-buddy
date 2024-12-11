using FluentValidation;

namespace Application.Words.Commands.CreateWord
{
    public sealed class CreateWordCommandValidator : AbstractValidator<CreateWordCommand>
    {
        public CreateWordCommandValidator() {
            RuleForEach(x => x.CreateWords)
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
