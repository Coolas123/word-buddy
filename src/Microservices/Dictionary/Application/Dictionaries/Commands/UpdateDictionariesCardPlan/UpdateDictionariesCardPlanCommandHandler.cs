using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Commands.UpdateDictionaryCardPlan
{
    internal class UpdateDictionariesCardPlanCommandHandler : ICommandHandler<UpdateDictionariesCardPlanCommand>
    {
        private readonly IDictionaryRepository dictionaryRepository;
        private readonly IUnitOfWork unitOfWork;
        public UpdateDictionariesCardPlanCommandHandler(IDictionaryRepository dictionaryRepository, IUnitOfWork unitOfWork) {
            this.dictionaryRepository = dictionaryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDictionariesCardPlanCommand request, CancellationToken cancellationToken) {
            await dictionaryRepository.UpdateCaprdPlanIds(request.DictionariesId, request.NewCardPlanId);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
