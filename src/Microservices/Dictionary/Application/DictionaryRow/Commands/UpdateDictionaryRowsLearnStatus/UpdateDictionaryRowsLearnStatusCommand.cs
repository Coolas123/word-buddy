using Application.Abstractions.Messaging;
using Application.DictionaryRow.Commands.UpdateDictionaryRowLearnStatus;

namespace Application.DictionaryRow.Commands.UpdateLearnStatus
{
    public sealed class UpdateDictionaryRowsLearnStatusCommand : ICommand
    {
        public IEnumerable<UpdateDictionaryRowLearnStatusCommand> DictionaryRows { get; set; } = Enumerable.Empty<UpdateDictionaryRowLearnStatusCommand>();
    }
}
