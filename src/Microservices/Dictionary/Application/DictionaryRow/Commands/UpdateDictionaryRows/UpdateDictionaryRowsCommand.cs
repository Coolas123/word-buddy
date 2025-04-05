using Application.Abstractions.Messaging;

namespace Application.DictionaryRow.Commands.UpdateWord.UpdateWords
{
    public sealed class UpdateDictionaryRowsCommand : ICommand
    {
        public List<UpdateDictionaryRowCommand> DictionaryRows { get; set; } = null!;
        public Guid DictionaryId { get; set; }
    }
}
