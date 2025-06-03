using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Shared;
using MediatR;

namespace Application.CardPlan.Commands.UpdateCardPlanAndDicitonaries
{
    internal class UpdateCardPlanAndDicitonariesCommandHandler : ICommandHandler<UpdateCardPlanAndDicitonariesCommand>
    {
        private readonly ISender sender;

        public UpdateCardPlanAndDicitonariesCommandHandler(ISender sender) {
            this.sender = sender;
        }

        public async Task<Result> Handle(UpdateCardPlanAndDicitonariesCommand request, CancellationToken cancellationToken) {
            var updateDeleteResult = await sender.Send(request.UpdateDeleteDictionariesCardPlanCommand);

            if (updateDeleteResult.IsFailure){
                return Result.Failure<string>(ApplicationError.CardPlan.UpdateFailure);
            }

            var updateNewResult = await sender.Send(request.UpdateNewDictionariesCardPlanCommand);

            if (updateNewResult.IsFailure) {
                return Result.Failure<string>(ApplicationError.CardPlan.UpdateFailure);
            }

            var updateCardPlanResult = await sender.Send(request.UpdateCardPlanCommand);

            if (updateCardPlanResult.IsFailure) {
                return Result.Failure<string>(ApplicationError.CardPlan.UpdateFailure);
            }

            return Result.Success();
        }
    }
}
