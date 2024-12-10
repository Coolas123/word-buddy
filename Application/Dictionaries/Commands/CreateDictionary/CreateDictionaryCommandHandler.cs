using Application.Abstractions.Messaging;
using Domain.Shared;

namespace Application.Dictionaries.Commands.CreateDictionary
{
    internal sealed class CreateDictionaryCommandHandler : ICommandHandler<CreateDictionaryCommand>
    {
        public Task<Result> Handle(CreateDictionaryCommand request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
