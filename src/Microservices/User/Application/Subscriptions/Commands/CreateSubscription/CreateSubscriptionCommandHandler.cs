using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Subscriptions.Commands.CreateSubscription
{
    internal class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork) {
            this.subscriptionRepository = subscriptionRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken) {
            await subscriptionRepository.CreateAsync(
                new Subscription(request.UserId, request.SubscriptionType,request.SubscribedAt,request.TokenLeft));

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
