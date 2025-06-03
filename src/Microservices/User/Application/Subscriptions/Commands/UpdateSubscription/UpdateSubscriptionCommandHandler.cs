using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Subscriptions.Commands.UpdateSubscription
{
    internal class UpdateSubscriptionCommandHandler : ICommandHandler<UpdateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        public UpdateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository) {
            this.subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken) {
            subscriptionRepository.Update(
                new Subscription(request.UserId,request.SubscriptionType,request.SubscribedAt,request.TokenLeft));

            return Result.Success();
        }
    }
}
