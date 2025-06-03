using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Subscriptions.Commands.UpdateSubscription
{
    public sealed class UpdateSubscriptionCommand : ICommand
    {
        public Guid UserId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime? SubscribedAt { get; set; }
        public int TokenLeft { get; set; }
    }
}
