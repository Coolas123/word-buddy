using Domain.Common.Enums;

namespace MassTransit.DTO.Dictionary.GetDictionaries
{
    public class DictionaryViewModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Language WordLanguage { get; set; }
        public Language TranslationLanguage { get; set; }
        public DateTime LastViewedAt { get; set; }
    }
}
