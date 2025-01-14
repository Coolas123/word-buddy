namespace Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; private init; }

        public Entity(Guid id) {
            Id = id;
        }

        public override bool Equals(object? obj) {
            if (obj == this) {
                return true;
            }

            if(obj is null) {
                return false;
            }

            if(obj.GetType() != GetType()) {
                return false;
            }

            return Id == ((Entity)obj).Id;
        }

        public bool Equals(Entity? other) {
            if(other == this) {
                return true;
            }

            if (other is null) {
                return false;
            }

            if (other.GetType() != GetType()) {
                return false;
            }

            return Id == other.Id;
        }
    }
}
