using Application.Abstractions.Messaging;
using Application.CardPlan.Commands.CreateCardPlan;
using Application.Dictionaries.Commands.UpdateDictionaryCardPlan;

namespace Application.CardPlan.Commands.CreateCardPlanAndUpdateDictionaryCardPlan
{
    public sealed class CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand : ICommand
    {
        public CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand(
            CreateCardPlanCommand createCardPlanCommand,
            UpdateDictionariesCardPlanCommand updateDictionariesCardPlanCommand) {
            CreateCardPlanCommand = createCardPlanCommand;
            UpdateDictionariesCardPlanCommand = updateDictionariesCardPlanCommand;
        }

        public CreateCardPlanCommand CreateCardPlanCommand { get; set; } = null!;
        public UpdateDictionariesCardPlanCommand UpdateDictionariesCardPlanCommand { get; set; } = null!;

        public static CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand Create(
            string title, string? descrition, IEnumerable<string> dictionariesId, Guid userID) {

            var updateDictionary = new UpdateDictionariesCardPlanCommand();
            updateDictionary.SetDictionariesId(dictionariesId);

            return new CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand(
                new CreateCardPlanCommand(title, descrition, userID),
                updateDictionary
            );
        }
    }
}
