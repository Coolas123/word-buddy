using Domain.Common.Enums;
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
        public DateTime LastViewedAt { get; init; }
        private List<DictionaryRow> _dictionaryRows = new();
        public IReadOnlyCollection<DictionaryRow> DictionaryRows => _dictionaryRows;

        public Dictionary(
            Guid id,
            Guid userId,
            string title, 
            string description,
            Language wordLanguage,
            Language translationLanguage,
            DateTime lastViewedAt) : base(id)
        {
            UserId = userId;
            Title = title;
            Description = description;
            WordLanguage = wordLanguage;
            TranslationLanguage = translationLanguage;
            LastViewedAt = lastViewedAt;
        }
    }
}
