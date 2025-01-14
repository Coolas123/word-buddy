using Application.Abstractions.Messaging;

namespace Application.Words.Commands.UpdateWord.UpdateWords
{
    public sealed class UpdateWordsCommand : ICommand
    {
        public List<UpdateWordCommand> Words { get; set; }
        public Guid DictionaryId { get; set; }
    }
}
