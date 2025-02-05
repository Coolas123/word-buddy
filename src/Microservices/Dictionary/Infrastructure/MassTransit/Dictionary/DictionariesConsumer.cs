using Application.Dictionaries.Queries.GetDictionaries;
using MassTransit;
using MassTransit.DTO.Dictionary.GetDictionaries;
using MediatR;

namespace Infrastructure.MassTransit.Dictionary
{
    public class DictionariesConsumer : IConsumer<DictionariesRequest>
    {
        public readonly ISender sender;

        public DictionariesConsumer(ISender sender) {
            this.sender = sender;
        }

        public async Task Consume(ConsumeContext<DictionariesRequest> context) {
            var dictionariesResult = await sender.Send(new GetDictionariesQuery {UserId= context.Message.UserId });

            if (dictionariesResult.IsFailure) {
                return;
            }

            var response = new DictionariesResponse
            {
                Dictionaries = dictionariesResult.Value().Select(x=> new DictionaryViewModel
                {
                    UserId = x.UserId,
                    Title = x.Title,
                    Description=x.Description,
                    WordLanguage = x.WordLanguage,
                    TranslationLanguage = x.TranslationLanguage,
                    LastViewedAt = x.LastViewedAt
                })
            };

            await context.RespondAsync(response);
        }
    }
}
