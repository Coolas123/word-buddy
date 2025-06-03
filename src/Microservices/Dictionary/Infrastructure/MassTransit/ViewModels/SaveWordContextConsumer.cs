using Domain.Repositories;
using MassTransit;
using MassTransit.Configuration.ViewModels;

namespace Infrastructure.MassTransit.ViewModels
{
    public class SaveWordContextConsumer : IConsumer<SaveWordContextRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private IDictionaryRepository dictionaryRepository;

        public SaveWordContextConsumer(IUnitOfWork unitOfWork, IDictionaryRepository dictionaryRepository) {
            this.unitOfWork = unitOfWork;
            this.dictionaryRepository = dictionaryRepository;
        }
        public async Task Consume(ConsumeContext<SaveWordContextRequest> context) {
            await dictionaryRepository.InsertWordContextAsync(context.Message.DictionaryId, context.Message.DictionaryRowId, context.Message.WordContext);
            
            await unitOfWork.SaveChangesAsync();
            var r = new SWCResponse();
            r.res = true;
            context.Respond(r);
        }
    }
}
