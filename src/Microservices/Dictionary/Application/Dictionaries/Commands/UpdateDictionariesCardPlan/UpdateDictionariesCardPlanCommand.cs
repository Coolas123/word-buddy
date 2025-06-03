using Application.Abstractions.Messaging;

namespace Application.Dictionaries.Commands.UpdateDictionaryCardPlan
{
    public sealed class UpdateDictionariesCardPlanCommand : ICommand
    {
        public Guid? NewCardPlanId { get; set; }
        public IEnumerable<Guid> DictionariesId { get; set; } 

        public UpdateDictionariesCardPlanCommand() { }

        public UpdateDictionariesCardPlanCommand(Guid? newCardPlanId, IEnumerable<string> dictionariesId) {
            NewCardPlanId = newCardPlanId;
            SetDictionariesId(dictionariesId);
        }

        public void SetDictionariesId(IEnumerable<string> dictionariesId) {
            DictionariesId = dictionariesId.Select(x=> Guid.Parse(x));
        }
    }
}
