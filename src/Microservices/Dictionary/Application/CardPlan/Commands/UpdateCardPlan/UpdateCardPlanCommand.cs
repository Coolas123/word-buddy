using Application.Abstractions.Messaging;

namespace Application.CardPlan.Commands.UpdateCardPlan
{
    public sealed class UpdateCardPlanCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public UpdateCardPlanCommand(Guid id, Guid userId, string title, string? description) {
            Id = id;
            UserId = userId;
            Title = title;
            Description = description;
        }
    }
}
