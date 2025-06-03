using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardPlan.Commands.UpdateCardPlan
{
    internal class UpdateCardPlanCommandHandler : ICommandHandler<UpdateCardPlanCommand>
    {
        private readonly ICardPlanRepository cardPlanRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCardPlanCommandHandler(ICardPlanRepository cardPlanRepository, IUnitOfWork unitOfWork) {
            this.cardPlanRepository = cardPlanRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCardPlanCommand request, CancellationToken cancellationToken) {
            var r = new Domain.Entities.CardPlan(request.Id, request.UserId, request.Title, request.Description, DateTime.Now.ToUniversalTime());
            cardPlanRepository.Update(r);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
