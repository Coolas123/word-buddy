using Application.Abstractions.Messaging;

namespace Application.CardPlan.Commands.CreateCardPlan
{
    public sealed class CreateCardPlanCommand : ICommand<Guid>
    {
        public CreateCardPlanCommand(string title, string description,Guid userId) {
            Title = title;
            Description = description;
            UserId = userId;
        }

        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
