using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Words.Commands.UpdateWord.UpdateWords
{
    internal class UpdateWordsCommandValidator : AbstractValidator<UpdateWordsCommand>
    {
        public UpdateWordsCommandValidator()
        {
            RuleForEach(x => x.Words)
                .Cascade(CascadeMode.Stop)
                .ChildRules(w => {
                    w.RuleFor(x => x.Text)
                    .NotEmpty()
                    .WithMessage("Введите слово");
                });
        }
    }
}
