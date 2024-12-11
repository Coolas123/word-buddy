using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Dictionaries.Commands.CreateDictionary
{
    internal sealed class CreateDictionaryCommandHandler : ICommandHandler<CreateDictionaryCommand,Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionaryRepository dictionaryRepository;

        public CreateDictionaryCommandHandler(IUnitOfWork unitOfWork, IDictionaryRepository dictionaryRepository) {
            this.unitOfWork = unitOfWork;
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task<Result<Guid>> Handle(CreateDictionaryCommand request, CancellationToken cancellationToken) {
            var dictionaryGuid = Guid.NewGuid();

            var dictionary = new Dictionary(
                dictionaryGuid,
                request.UserId,
                request.Title,
                request.Description,
                (Language)request.WordLanguage,
                (Language)request.TranslationLanguage,
                (DateTime)request.CreatedAt);

            await dictionaryRepository.CreateAsync(dictionary);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(dictionaryGuid);
        }
    }
}
