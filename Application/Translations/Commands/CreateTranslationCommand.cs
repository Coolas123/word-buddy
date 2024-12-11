using Application.Abstractions.Messaging;

namespace Application.Translations.Commands
{
    public sealed class CreateTranslationCommand : ICommand
    {
        public List<CreateTranslation> CreateTranslations { get; set; } = new();
        public List<Guid>? WordGuids { get; set; }
    }

    public class CreateTranslation {
        public string? Text { get; set; }
        public Guid? WordId { get; set; }
    }
}
