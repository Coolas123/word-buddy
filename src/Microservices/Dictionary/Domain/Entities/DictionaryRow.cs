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
        public DateTime? CardBoxLearnStatusChangedAt { get; private set; }
        public string WordTranslation { get; private set; }
        public List<string>? WordContexts { get; private set; }
        public string? ImgPath { get; private set; }
        public string? NoteText { get; private set; }

        public DictionaryRow(
            Guid id, 
            Guid dictionaryId, 
            string wordText, 
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            DateTime? cardBoxLearnStatusChangedAt,
            string wordTranslation) : base(id) 
        {
            DictionaryId = dictionaryId;
            WordText = wordText;
            LearnStatus = learnStatus;
            LearnStatusChangedAt = learnStatusChangedAt;
            CardBoxLearnStatusChangedAt = cardBoxLearnStatusChangedAt;
            WordTranslation = wordTranslation;
        }
        private DictionaryRow(
            Guid id,
            Guid dictionaryId,
            string wordText,
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            string WordTranslation,
            List<string> wordContexts,
            string? imgPath,
            string? noteText,
            DateTime? cardBoxLearnStatusChangedAt=null) : this(id, dictionaryId, wordText, learnStatus, learnStatusChangedAt, cardBoxLearnStatusChangedAt, WordTranslation) {
            WordContexts = wordContexts;
            ImgPath = imgPath;
            NoteText = noteText;
        }


        private DictionaryRow(Guid id) : base(id) {
        }

        public static DictionaryRow Create(
            Guid id,
            Guid dictionaryId,
            string wordText,
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt,
            string WordTranslation,
            List<string>? wordContexts,
            string? imgPath,
            string? noteText,
            DateTime? cardBoxLearnStatusChangedAt = null) {
            return new DictionaryRow(id, dictionaryId, wordText, learnStatus, learnStatusChangedAt, WordTranslation, wordContexts, imgPath, noteText);
        }

        public static DictionaryRow CreateForAttachingToDb(Guid id) {
            return new DictionaryRow(id);
        }

        public void AddWordContext(string wordContext) {
            WordContexts.Add(wordContext);
        }
        
        public void ChangeRowsDictionaryBelong(Guid newDictionaryId) {
            DictionaryId = newDictionaryId;
        }

        public void ChangeLearnStatus(LearnStatus learnStatus) {
            LearnStatus = learnStatus;
        }

        public void UpdateCardBoxLearnStatusChangedAt(DateTime cardBoxLearnStatusChangedAt) {
            CardBoxLearnStatusChangedAt = cardBoxLearnStatusChangedAt;
        }

        public void UpdateLearnStatusChangedAt(DateTime learnStatusChangedAt) {
            LearnStatusChangedAt = learnStatusChangedAt;
        }
    }
}
