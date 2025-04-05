using Application.Abstractions.Messaging;
using Domain.Shared;
using MediatR;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    internal sealed class CreateDictionaryAndRowsCommandHandler : ICommandHandler<CreateDictionaryAndRowsCommand>
    {
        private readonly ISender sender;

        public CreateDictionaryAndRowsCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result> Handle(CreateDictionaryAndRowsCommand request, CancellationToken cancellationToken) {
            var createDictionaryResult = await sender.Send(request.CreateDictionaryCommand);

            if (createDictionaryResult.IsFailure){
                return createDictionaryResult;
            }

            request.CreateDictionaryRowsCommand.DictionaryId = createDictionaryResult.Value();

            var createWordResult = await sender.Send(request.CreateDictionaryRowsCommand);

            if (createWordResult.IsFailure) {
                return createWordResult;
            }

            return Result.Success();
        }
    }
}
