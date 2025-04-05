using Application.Abstractions.Messaging;

namespace Application.DictionaryRow.Commands.CreateWord
{
    public sealed class CreateDictionaryRowsCommand : ICommand<List<Guid>>
    {
        public List<CreateDictionaryRowCommand> DictionaryRows { get; set; } = new();
        public Guid? DictionaryId { get; set; }
    }
}
