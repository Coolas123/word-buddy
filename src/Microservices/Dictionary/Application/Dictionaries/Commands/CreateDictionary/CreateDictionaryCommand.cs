using Application.Abstractions.Messaging;
using Domain.Common.Enums;

namespace Application.Dictionaries.Commands.CreateDictionary
{
    public sealed class CreateDictionaryCommand : ICommand<Guid>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Language? WordLanguage { get; set; }
        public Language? TranslationLanguage { get; set; }

        public DateTime? LastViewedAt { get; set; }

        public Guid UserId { get; set; }
    }
}
