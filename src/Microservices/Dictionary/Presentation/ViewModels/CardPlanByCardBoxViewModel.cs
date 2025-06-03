using Domain.Entities;

namespace Presentation.ViewModels
{
    public class CardPlanByCardBoxViewModel
    {
        public Guid CardPlanId { get; set; }
        public IEnumerable<Guid> CardBoxDictionariesId { get; set; } = Enumerable.Empty<Guid>();

        public CardPlanByCardBoxViewModel(Guid cardPlanId, IEnumerable<Guid> cardBoxDictionariesId) {
            CardPlanId = cardPlanId;
            CardBoxDictionariesId = cardBoxDictionariesId;
        }
    }
}
