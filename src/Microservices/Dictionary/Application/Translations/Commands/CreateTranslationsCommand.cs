using Application.Abstractions.Messaging;
using Application.Translations.Commands.CreateTranslation;

namespace Application.Translations.Commands
{
    public sealed class CreateTranslationsCommand : ICommand
    {
        public List<CreateTranslationCommand> CreateTranslations { get; set; } = new();
        public List<Guid>? WordGuids { get; set; }
    }
}
