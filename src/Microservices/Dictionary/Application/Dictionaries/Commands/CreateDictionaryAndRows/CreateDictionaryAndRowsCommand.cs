using Application.Abstractions.Messaging;
using Application.Dictionaries.Commands.CreateDictionary;
using Application.Translations.Commands;
using Application.Words.Commands.CreateWord;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    public sealed class CreateDictionaryAndRowsCommand : ICommand
    {
        public CreateWordsCommand CreateWordsCommand { get; set; } = new();
        public CreateDictionaryCommand CreateDictionaryCommand { get; set; } = new();
    }
}
