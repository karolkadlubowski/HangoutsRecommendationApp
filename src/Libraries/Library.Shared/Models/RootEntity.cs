using System;
using System.Collections.Generic;
using System.Linq;
using Library.EventBus;

namespace Library.Shared.Models
{
    public abstract class RootEntity
    {
        private readonly Queue<Event> _domainEvents = new Queue<Event>();

        public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; protected set; }

        public void UpdateNow() => ModifiedOn = DateTime.UtcNow;

        public Event FirstStoredEvent
            => _domainEvents.Any()
                ? _domainEvents.Peek()
                : null;

        public IReadOnlyList<Event> GetOrderedEvents()
        {
            var orderedEvents = new List<Event>();

            while (_domainEvents.Any())
                orderedEvents.Add(_domainEvents.Dequeue());

            return orderedEvents;
        }

        public void AddDomainEvent(Event @event) => _domainEvents.Enqueue(@event);
    }
}