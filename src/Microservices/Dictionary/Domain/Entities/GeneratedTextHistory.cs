using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class GeneratedTextHistory : Entity
    {
        public GeneratedTextHistory(Guid id) : base(id) {
        }

        public string Text { get; private set; } = null!;
        public Guid UserId { get; private set; }

        private GeneratedTextHistory(Guid id,string text,Guid userId) : base(id) {
            Text = text;
            UserId = userId;
        }

        public static GeneratedTextHistory? Create(Guid id, string text, Guid userId) {
            if (string.IsNullOrEmpty(text)) return null;
            return new GeneratedTextHistory(id, text, userId);
        }
    }
}
