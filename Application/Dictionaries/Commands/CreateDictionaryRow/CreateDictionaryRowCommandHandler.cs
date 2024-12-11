using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    internal sealed class CreateDictionaryRowCommandHandler : ICommandHandler<CreateDictionaryRowCommand>
    {
        private readonly ISender sender;

        public CreateDictionaryRowCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result> Handle(CreateDictionaryRowCommand request, CancellationToken cancellationToken) {
            var CreateWordResult =  await sender.Send(request.CreateWordCommand);

            if (CreateWordResult.IsFailure) {
                return CreateWordResult;
            }

            request.CreateTranslationCommand.WordGuids = CreateWordResult.Value();

            var CreateTranslationResult = await sender.Send(request.CreateTranslationCommand);

            if (CreateTranslationResult.IsFailure) {
                return CreateTranslationResult;
            }

            return Result.Success();
        }
    }
}
