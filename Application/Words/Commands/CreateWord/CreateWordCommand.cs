using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Words.Commands.CreateWord
{
    public sealed class CreateWordCommand : ICommand<List<Guid>>
    {
        public List<CreateWord> CreateWords { get; set; } = new();
        public Guid? DictionaryId { get; set; }
    }

    public sealed class CreateWord{
        public string? Text { get; set; }
        public LearnStatus? LearnStatus { get; set; }
    }
}
