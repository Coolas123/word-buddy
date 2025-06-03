using Domain.Common.Enums;
using Domain.Entities;

namespace Presentation.ViewModels
{
    public sealed class DictionariesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Language WordLanguage { get; set; }
        public Language TranslationLanguage { get; set; }
        public DateTime LastViewedAt { get; set; }

        public static IEnumerable<DictionariesViewModel> CreateArray(IEnumerable<Dictionary> dictionary) {
            return dictionary.Select(x => new DictionariesViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                WordLanguage = x.WordLanguage,
                TranslationLanguage = x.TranslationLanguage,
                LastViewedAt = x.LastViewedAt
            });
        }
    }
}
