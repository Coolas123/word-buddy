
using FluentValidation;

namespace Application.CardPlan.Commands.CreateCardPlan
{
    internal class CreateCardPlanCommandValidation : AbstractValidator<CreateCardPlanCommand>
    {
        public CreateCardPlanCommandValidation() {
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
        }
    }
}
