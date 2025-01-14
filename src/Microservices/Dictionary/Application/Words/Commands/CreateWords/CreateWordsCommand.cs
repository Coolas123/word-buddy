using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Words.Commands.CreateWord
{
    public sealed class CreateWordsCommand : ICommand<List<Guid>>
    {
        public List<CreateWordCommand> Words { get; set; } = new();
        public Guid? DictionaryId { get; set; }
    }
}
