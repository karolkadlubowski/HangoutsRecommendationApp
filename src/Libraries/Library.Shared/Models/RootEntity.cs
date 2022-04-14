using System;
using System.Collections.Generic;
using System.Linq;
using Library.EventBus;

namespace Library.Shared.Models
{
    public abstract class RootEntity
    {
        public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; protected set; }

        public Queue<Event> DomainEvents { get; protected set; } = new Queue<Event>();

        public void UpdateNow() => ModifiedOn = DateTime.UtcNow;

        public Event FirstStoredEvent => DomainEvents.Peek();

        public IReadOnlyList<Event> GetOrderedEvents()
        {
            var orderedEvents = new List<Event>();

            while (DomainEvents.Any())
                orderedEvents.Add(DomainEvents.Dequeue());

            return orderedEvents;
        }

        public void AddDomainEvent(Event @event) => DomainEvents.Enqueue(@event);
    }
}