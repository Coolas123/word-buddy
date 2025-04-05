using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.DictionaryRow.Commands.CreateWord
{
    public sealed class CreateDictionaryRowCommand : ICommand
    {
        public string WordText { get; set; } = null!;
        public string WordTranslation { get; set; } = null!;
        public LearnStatus LearnStatus { get; set; }
        public List<string> WordContexts { get; set; } = new();
    }
}
