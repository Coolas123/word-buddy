using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Translations.Commands
{
    public sealed class CreateTranslationsCommandHandler : ICommandHandler<CreateTranslationsCommand>
    {
        private readonly ITranslationRepository translationRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateTranslationsCommandHandler(ITranslationRepository translationRepository,
            IUnitOfWork unitOfWork) {
            this.translationRepository = translationRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CreateTranslationsCommand request, CancellationToken cancellationToken) {
            var wordsForSave = new List<Translation>(request.CreateTranslations.Count);
            for (int i = 0; i < request.CreateTranslations.Count; i++) {
                wordsForSave.Add(new Translation(
                request.WordGuids[i],
                request.CreateTranslations[i].Text));
            }

            translationRepository.CreateRange(wordsForSave);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
