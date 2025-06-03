using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.CardBox.Commands.CreateCardBoxesByDefault
{
    public sealed class CreateCardBoxesByDefaultCommand : ICommand
    {
        public CreateCardBoxesByDefaultCommand(Guid cardPlanId, IEnumerable<Guid> dictionariesId) {
            CardPlanId=cardPlanId;
            DictionariesId=dictionariesId;
        }

        public Guid CardPlanId { get; set; }
        public IEnumerable<Guid> DictionariesId { get; set; } = Enumerable.Empty<Guid>();
    }
}
