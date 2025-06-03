using MediatR;

namespace Domain.Primitives
{
    public class DomainEvent : INotification
    {
        public Guid Id { get; private set; }
        private readonly List<DomainEvent> _domainEvents = new();
        public List<DomainEvent> DomainEvents => _domainEvents;

        public DomainEvent(Guid id) {
            Id = id;
        }

        public void AddDomainEvent(DomainEvent domainEvent) {
            _domainEvents.Add(domainEvent);
        }
    }
}
