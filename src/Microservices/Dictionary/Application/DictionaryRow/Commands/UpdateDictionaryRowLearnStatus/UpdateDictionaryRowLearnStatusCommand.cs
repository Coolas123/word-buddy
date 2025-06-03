using Domain.Enums;

namespace Application.DictionaryRow.Commands.UpdateDictionaryRowLearnStatus
{
    public sealed class UpdateDictionaryRowLearnStatusCommand
    {
        public Guid DictionaryRowId { get; set; }
        public LearnStatus NewLearnStatus { get; set; }
        public bool IsCardPlan { get; set; }
    }
}
