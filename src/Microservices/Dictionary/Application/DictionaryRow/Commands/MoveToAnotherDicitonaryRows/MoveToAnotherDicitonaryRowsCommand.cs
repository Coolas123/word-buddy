using Application.Abstractions.Messaging;

namespace Application.DictionaryRow.Commands.MoveToAnotherDicitonaryRows
{
    public sealed class MoveToAnotherDicitonaryRowsCommand : ICommand
    {
        public Guid DictionaryTargetId { get; set; }
        public IEnumerable<Guid> WordsId { get; set; } = null!;
    }
}
