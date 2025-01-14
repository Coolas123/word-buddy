using Application.Abstractions.Messaging;
using Application.Translations.Commands.CreateTranslation;
using Domain.Enums;

namespace Application.Words.Commands.CreateWord
{
    public sealed class CreateWordCommand : ICommand
    {
        public string? Text { get; set; }
        public LearnStatus? LearnStatus { get; set; }
        public CreateTranslationCommand Translation { get; set; }
    }
}
