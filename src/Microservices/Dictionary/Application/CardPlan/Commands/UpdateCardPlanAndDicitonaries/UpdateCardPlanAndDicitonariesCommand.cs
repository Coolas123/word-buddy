using Application.Abstractions.Messaging;
using Application.CardPlan.Commands.UpdateCardPlan;
using Application.Dictionaries.Commands.UpdateDictionaryCardPlan;

namespace Application.CardPlan.Commands.UpdateCardPlanAndDicitonaries
{
    public sealed class UpdateCardPlanAndDicitonariesCommand : ICommand
    {
        public UpdateCardPlanCommand UpdateCardPlanCommand { get; set; } = null!;
        public UpdateDictionariesCardPlanCommand UpdateNewDictionariesCardPlanCommand { get; set; } = null!;
        public UpdateDictionariesCardPlanCommand UpdateDeleteDictionariesCardPlanCommand { get; set; } = null!;
    }
}
