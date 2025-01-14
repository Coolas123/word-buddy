using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Commands.UpdateDictionary
{
    internal class UpdateDictionaryCommandHandler : ICommandHandler<UpdateDictionaryCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionaryRepository dictionaryRepository;

        public UpdateDictionaryCommandHandler(IUnitOfWork unitOfWork, IDictionaryRepository dictionaryRepository) {
            this.unitOfWork = unitOfWork;
            this.dictionaryRepository = dictionaryRepository;
        }

        public async Task<Result> Handle(UpdateDictionaryCommand request, CancellationToken cancellationToken) {
            var updatedDictionary = new Dictionary(
                (Guid)request.DictionaryId,
                (Guid)request.UserId,
                request.Title,
                request.Description,
                (Language)request.WordLanguage,
                (Language)request.TranslationLanguage,
                (DateTime)request.LastViewedAt);

            dictionaryRepository.Update(updatedDictionary);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
