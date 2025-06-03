using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.DictionaryRow.Commands.UpdateLearnStatus
{
    internal class UpdateDictionaryRowsLearnStatusCommandHandler : ICommandHandler<UpdateDictionaryRowsLearnStatusCommand>
    {
        private readonly IDictionaryRowRepository dictionaryRowRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdateDictionaryRowsLearnStatusCommandHandler(IDictionaryRowRepository dictionaryRowRepository, IUnitOfWork unitOfWork) {
            this.dictionaryRowRepository = dictionaryRowRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDictionaryRowsLearnStatusCommand request, CancellationToken cancellationToken) {
            await dictionaryRowRepository.UpdateLearnStatus(request.DictionaryRows.Select(x=>(x.DictionaryRowId, x.NewLearnStatus,DateTime.Now.ToUniversalTime(),x.IsCardPlan)));
            
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
