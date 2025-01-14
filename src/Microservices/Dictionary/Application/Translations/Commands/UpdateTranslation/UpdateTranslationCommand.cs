using Application.Abstractions.Messaging;

namespace Application.Translations.Commands.UpdateTranslation
{
    public sealed class UpdateTranslationCommand : ICommand
    {
        public Guid WordId { get; set; }
        public string Text { get; set; }
    }
}
