using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Subscription : Entity
    {
        public Guid UserId { get; set; }
        public SubscriptionType SubscriptionType {  get; set; }
        public DateTime? SubscribedAt { get; set; } 
        public int TokenLeft { get; set; }

        public Subscription(Guid userId,
            SubscriptionType subscriptionType,
            DateTime? subscribedAt,
            int tokenLeft) : base(userId) {
            UserId = userId;
            SubscriptionType = subscriptionType;
            SubscribedAt = subscribedAt;
            TokenLeft = tokenLeft;
        }
    }
}
