using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class DictionaryRow : Entity
    {
        public Guid DictionaryId { get; private set; }
        public string WordText { get; private set; }
        public LearnStatus LearnStatus  { get; private set; }
        public DateTime LearnStatusChangedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string WordTranslation { get; private set; }
        public List<string> WordContexts { get; private set; }

        public DictionaryRow(
            Guid id, 
            Guid dictionaryId, 
            string wordText, 
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            DateTime createdAt,
            string wordTranslation) : base(id) 
        {
            DictionaryId = dictionaryId;
            WordText = wordText;
            LearnStatus = learnStatus;
            LearnStatusChangedAt = learnStatusChangedAt;
            CreatedAt = createdAt;
            WordTranslation = wordTranslation;
        }
        private DictionaryRow(
            Guid id,
            Guid dictionaryId,
            string wordText,
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            DateTime createdAt,
            string WordTranslation,
            List<string> wordContexts) : this(id, dictionaryId, wordText, learnStatus, learnStatusChangedAt, createdAt, WordTranslation) {
            WordContexts = wordContexts;
        }

        public static DictionaryRow Create(
            Guid id,
            Guid dictionaryId,
            string wordText,
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            DateTime createdAt,
            string WordTranslation,
            List<string>? wordContexts) {
            return new DictionaryRow(id, dictionaryId, wordText, learnStatus, learnStatusChangedAt, createdAt, WordTranslation, wordContexts);
        }
    }
}
