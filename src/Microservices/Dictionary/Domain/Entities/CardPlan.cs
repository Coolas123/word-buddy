using Domain.Primitives;

namespace Domain.Entities
{
    public class CardPlan : Entity
    {
        private List<Dictionary> _dictionaries = new();
        private List<CardBox> _cardBoxes = new();
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime ViewedAt { get; private set; }
        public IReadOnlyCollection<Dictionary> Dictionaries => _dictionaries;
        public IReadOnlyCollection<CardBox> CardBoxes => _cardBoxes;

        public CardPlan(
            Guid id,
            Guid userId,
            string title,
            string description,
            DateTime viewedAt) : base(id) 
        {
            UserId = userId;
            Title = title;
            Description = description;
            ViewedAt = viewedAt;
        }

        public CardPlan(
            Guid id,
            string title,
            string description,
            DateTime viewedAt) : base(id) {
            Title = title;
            Description = description;
            ViewedAt = viewedAt;
        }
    }
}
