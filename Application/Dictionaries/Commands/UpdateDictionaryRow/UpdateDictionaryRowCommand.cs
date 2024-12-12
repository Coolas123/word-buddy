using Application.Abstractions.Messaging;
using Application.Words.Commands.UpdateWord;

namespace Application.Dictionaries.Commands.UpdateDictionaryRow
{
    public sealed class UpdateDictionaryRowCommand : ICommand
    {
        public IEnumerable<UpdateWordCommand> Words { get; set; }
    }
}
