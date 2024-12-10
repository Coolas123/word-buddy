using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class Word : Entity
    {
        public Guid DictionaryId { get; private set; }
        public string Text { get; private set; }
        public LearnStatus LearnStatus  { get; private set; }
        public DateTime LearnStatusChangedAt { get; private set; }
        public Translation Translation { get; private set; }

        public Word(
            Guid id, 
            Guid dictionaryId, 
            string text, 
            LearnStatus learnStatus,
            DateTime learnStatusChangedAt) : base(id) {
            DictionaryId = dictionaryId;
            Text = text;
            LearnStatus = learnStatus;
            LearnStatusChangedAt = learnStatusChangedAt;
        }
    }
}
