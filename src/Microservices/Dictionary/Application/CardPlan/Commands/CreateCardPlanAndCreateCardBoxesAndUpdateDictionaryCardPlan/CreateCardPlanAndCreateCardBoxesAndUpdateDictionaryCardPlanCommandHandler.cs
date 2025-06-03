using Application.Abstractions.Messaging;
using Application.CardBox.Commands.CreateCardBoxesByDefault;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.CardPlan.Commands.CreateCardPlanAndUpdateDictionaryCardPlan
{
    internal class CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommandHandler : ICommandHandler<CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand>
    {
        private readonly ISender sender;

        public CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result> Handle(CreateCardPlanAndCreateCardBoxesAndUpdateDictionaryCardPlanCommand request, CancellationToken cancellationToken) {
            var createCardPlanCommandResult = await sender.Send(request.CreateCardPlanCommand);

            if (createCardPlanCommandResult.IsFailure) {
                return createCardPlanCommandResult;
            }

            request.UpdateDictionariesCardPlanCommand.NewCardPlanId = createCardPlanCommandResult.Value();
            var updateDicitonaryCardPlanResult = await sender.Send(request.UpdateDictionariesCardPlanCommand);

            if (updateDicitonaryCardPlanResult.IsFailure) {
                return updateDicitonaryCardPlanResult;
            }

            var createCardBoxesResult = await sender.Send(
                new CreateCardBoxesByDefaultCommand(createCardPlanCommandResult.Value(), request.UpdateDictionariesCardPlanCommand.DictionariesId));

            if (createCardBoxesResult.IsFailure) {
                return createCardBoxesResult;
            }

            return Result.Success();
        }
    }
}
