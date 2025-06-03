using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.CardBox.Commands.CreateCardBoxesByDefault
{
    internal class CreateCardBoxesByDefaultCommandHandler : ICommandHandler<CreateCardBoxesByDefaultCommand>
    {
        private readonly ICardBoxRepository cardBoxRepository;
        private readonly IDictionaryRowRepository dictionaryRowRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateCardBoxesByDefaultCommandHandler(ICardBoxRepository cardBoxRepository,
            IDictionaryRowRepository dictionaryRowRepository,
            IUnitOfWork unitOfWork) {
            this.cardBoxRepository = cardBoxRepository;
            this.dictionaryRowRepository = dictionaryRowRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CreateCardBoxesByDefaultCommand request, CancellationToken cancellationToken) {
            var newCardBoxes = new List<Domain.Entities.CardBox>();

            var totalWords = await dictionaryRowRepository.CountTotalWords(request.DictionariesId);

            newCardBoxes.Add(Domain.Entities.CardBox.CreateByDefault(
                request.CardPlanId,
                LearnStatus.NotStudied,
                totalWords.ContainsKey(LearnStatus.NotStudied)? totalWords[LearnStatus.NotStudied]:0,
                1));
            newCardBoxes.Add(Domain.Entities.CardBox.CreateByDefault(
                request.CardPlanId,
                LearnStatus.InStudying,
                totalWords.ContainsKey(LearnStatus.InStudying) ? totalWords[LearnStatus.InStudying] : 0,
                3));
            newCardBoxes.Add(Domain.Entities.CardBox.CreateByDefault(
                request.CardPlanId,
                LearnStatus.NeedToRemember,
                totalWords.ContainsKey(LearnStatus.NeedToRemember) ? totalWords[LearnStatus.NeedToRemember] : 0,
                10));
            newCardBoxes.Add(Domain.Entities.CardBox.CreateByDefault(
                request.CardPlanId,
                LearnStatus.VeryDifficult,
                totalWords.ContainsKey(LearnStatus.VeryDifficult) ? totalWords[LearnStatus.VeryDifficult] : 0,
                null));

            await cardBoxRepository.CreateRangeAsync(newCardBoxes);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
