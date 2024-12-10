using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class Dictionary : Entity
    {
        public Guid UserId { get; init; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Language WordLanguage {  get; private set; }
        public Language TranslationLanguage {  get; private set; }
        public DateTime CreatedAt { get; init; }
        private List<Word> _words = new();
        public IReadOnlyCollection<Word> Words => _words;

        public Dictionary(
            Guid id,
            Guid userId,
            string title, 
            string description,
            Language wordLanguage,
            Language translationLanguage,
            DateTime createdAt) : base(id)
        {
            UserId = userId;
            Title = title;
            Description = description;
            WordLanguage = wordLanguage;
            TranslationLanguage = translationLanguage;
            CreatedAt = createdAt;
        }
    }
}
