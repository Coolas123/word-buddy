using Application.Abstractions.Messaging;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DictionaryRow.Commands.UpdateWord
{
    public sealed class UpdateDictionaryRowCommand : ICommand
    {
        public Guid Id { get; set; }
        public string WordText { get; set; } = null!;
        public LearnStatus LearnStatus { get; set; }
        public DateTime LearnStatusChangedAt { get; set; }
        public DateTime CardBoxLearnStatusChangedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string WordTranslation { get; set; } = null!;
        public List<string> WordContexts { get; set; } = null!;
        public string? ImgBase64 { get; set; } = null!;
        public string? ImgPath { get; set; } = null!;
        public string? NoteText { get; set; }
    }
}
