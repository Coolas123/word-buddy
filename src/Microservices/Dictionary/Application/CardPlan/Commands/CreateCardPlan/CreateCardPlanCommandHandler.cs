using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardPlan.Commands.CreateCardPlan
{
    internal class CreateCardPlanCommandHandler : ICommandHandler<CreateCardPlanCommand,Guid>
    {
        private readonly ICardPlanRepository cardPlanRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateCardPlanCommandHandler(ICardPlanRepository cardPlanRepository, IUnitOfWork unitOfWork) {
            this.cardPlanRepository = cardPlanRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateCardPlanCommand request, CancellationToken cancellationToken) {
            var newCardPlan = new Domain.Entities.CardPlan(
                Guid.NewGuid(),request.UserId,request.Title, request.Description, DateTime.Now.ToUniversalTime());

            await cardPlanRepository.CreateAsync(newCardPlan);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(newCardPlan.Id);
        }
    }
}
