namespace Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetAtomicValues();

        private bool ValuesAreEqual(ValueObject other) {
            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }

        public override bool Equals(object? obj) {
            if (obj is null) {
                return false;
            }
            if (obj is not ValueObject) {
                return false;
            }
            if (!ValuesAreEqual((ValueObject)obj)) {
                return false;
            }
            return true;
        }

        public override int GetHashCode() {
            return GetAtomicValues().Aggregate(default(int), HashCode.Combine);
        }

        public bool Equals(ValueObject? other) {
            if (other is null) {
                return false;
            }
            if (!ValuesAreEqual(other)) {
                return false;
            }
            return true;
        }
    }
}
