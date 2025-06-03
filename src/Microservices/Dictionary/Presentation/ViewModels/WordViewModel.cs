using Domain.Entities;
using Domain.Enums;

namespace Presentation.ViewModels
{
    public sealed class WordViewModel
    {
        public Guid DictionaryId { get; private set; }
        public string WordText { get; private set; }
        public LearnStatus LearnStatus { get; private set; }
        public DateTime LearnStatusChangedAt { get; private set; }
        public DateTime? CardBoxLearnStatusChangedAt { get; private set; }
        public string WordTranslation { get; private set; }
        public IEnumerable<string> WordContexts { get; private set; }

        public static List<WordViewModel> CreateArray(IEnumerable<DictionaryRow> words) {
            return words.Select(x => new WordViewModel
            {
                DictionaryId = x.DictionaryId,
                WordText = x.WordText,
                LearnStatus = x.LearnStatus,
                LearnStatusChangedAt = x.LearnStatusChangedAt,
                CardBoxLearnStatusChangedAt = x.CardBoxLearnStatusChangedAt,
                WordTranslation = x.WordTranslation,
            }).ToList();
        }
    }
}
