using Domain.Common.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class Dictionary : Entity
    {
        private List<DictionaryRow> _dictionaryRows = new();
        public Guid UserId { get; init; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Language WordLanguage {  get; private set; }
        public Language TranslationLanguage {  get; private set; }
        public DateTime LastViewedAt { get; init; }
        public Guid? CardPlanId {  get; private set; }
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

        public void AddWordContextToWord(Guid dictionaryRowId, string wordContext) {
            var r = _dictionaryRows.Where(x => x.DictionaryId == dictionaryRowId).Select(x => { x.AddWordContext(wordContext); return x; });
        }

        public void ChangeCardPlanId(Guid? cardPlanId) {
            CardPlanId = cardPlanId;
        }
    }
}
