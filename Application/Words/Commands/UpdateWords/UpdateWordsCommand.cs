using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;

namespace Application.Words.Commands.UpdateWord.UpdateWords
{
    public sealed class UpdateWordsCommand : ICommand
    {
        public List<UpdateWordCommand> Words { get; set; }
        public Guid DictionaryId { get; set; }
    }
}
