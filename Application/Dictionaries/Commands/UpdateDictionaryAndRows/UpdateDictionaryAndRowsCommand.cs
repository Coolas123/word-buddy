using Application.Abstractions.Messaging;
using Application.Dictionaries.Commands.UpdateDictionary;
using Application.Words.Commands.CreateWord;
using Application.Words.Commands.UpdateWord.UpdateWords;

namespace Application.Dictionaries.Commands.UpdateDictionaryAndRows
{
    public sealed class UpdateDictionaryAndRowsCommand : ICommand
    {
        public UpdateWordsCommand? UpdateWordsCommand { get; set; }
        public CreateWordsCommand? CreateWordsCommand { get; set; } = new();
        public UpdateDictionaryCommand? UpdateDictionaryCommand { get; set; }
    }
}
