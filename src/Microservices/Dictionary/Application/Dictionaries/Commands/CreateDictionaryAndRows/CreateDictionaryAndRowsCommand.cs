using Application.Abstractions.Messaging;
using Application.Dictionaries.Commands.CreateDictionary;
using Application.DictionaryRow.Commands.CreateWord;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    public sealed class CreateDictionaryAndRowsCommand : ICommand
    {
        public CreateDictionaryRowsCommand CreateDictionaryRowsCommand { get; set; } = null!;
        public CreateDictionaryCommand CreateDictionaryCommand { get; set; } = null!;
    }
}
