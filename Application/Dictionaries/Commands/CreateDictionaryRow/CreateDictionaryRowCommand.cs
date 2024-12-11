using Application.Abstractions.Messaging;
using Application.Translations.Commands;
using Application.Words.Commands.CreateWord;

namespace Application.Dictionaries.Commands.CreateDictionaryRow
{
    public sealed class CreateDictionaryRowCommand : ICommand
    {
        public CreateWordCommand CreateWordCommand { get; set; } = new();
        public CreateTranslationCommand CreateTranslationCommand { get; set; } = new();
    }
}
