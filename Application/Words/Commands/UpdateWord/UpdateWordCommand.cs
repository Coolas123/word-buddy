using Application.Abstractions.Messaging;
using Application.Translations.Commands.UpdateTranslation;
using Domain.Enums;

namespace Application.Words.Commands.UpdateWord
{
    public sealed class UpdateWordCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public LearnStatus LearnStatus { get; set; }
        public DateTime LearnStatusChangedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public UpdateTranslationCommand Translation { get; set; }
    }
}
