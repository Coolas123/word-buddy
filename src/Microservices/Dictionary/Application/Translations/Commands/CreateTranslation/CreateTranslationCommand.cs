
using Application.Abstractions.Messaging;

namespace Application.Translations.Commands.CreateTranslation
{
    public sealed class CreateTranslationCommand : ICommand
    {
        public string Text { get; set; }
    }
}
