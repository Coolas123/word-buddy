using Application.Abstractions.Messaging;
using Application.Dictionaries.Commands.UpdateDictionary;
using Application.DictionaryRow.Commands.CreateWord;
using Application.DictionaryRow.Commands.UpdateWord.UpdateWords;

namespace Application.Dictionaries.Commands.UpdateDictionaryAndRows
{
    public sealed class UpdateDictionaryAndRowsCommand : ICommand
    {
        public UpdateDictionaryRowsCommand UpdateDictionaryRowsCommand { get; set; } = null!;
        public CreateDictionaryRowsCommand CreateDictionaryRowsCommand { get; set; } = null!;
        public UpdateDictionaryCommand UpdateDictionaryCommand { get; set; } = null!;
    }
}
