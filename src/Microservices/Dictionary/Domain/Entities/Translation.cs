using Domain.Primitives;

namespace Domain.Entities
{
    public class Translation : Entity
    {
        public Guid WordId { get; private set; }
        public string Text { get; private set; }
        public Translation(Guid wordId, string text) : base(wordId) {
            WordId = wordId;
            Text = text;
        }
    }
}
