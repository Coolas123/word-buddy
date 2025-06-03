using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.GeneratedTextHistory.Commands.SaveGeneratedTextHistory
{
    internal class SaveGeneratedTextHistoryCommandHandler : ICommandHandler<SaveGeneratedTextHistoryCommand>
    {
        private readonly IGeneratedTextHistoryRepository generatedTextHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public SaveGeneratedTextHistoryCommandHandler(IGeneratedTextHistoryRepository generatedTextHistoryRepository, IUnitOfWork unitOfWork) {
            this.generatedTextHistoryRepository = generatedTextHistoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(SaveGeneratedTextHistoryCommand request, CancellationToken cancellationToken) {
            await generatedTextHistoryRepository.CreateAsync(
                Domain.Entities.GeneratedTextHistory.Create(Guid.NewGuid(), request.Text, request.UserId));

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
