using System;
using System.Threading;
using System.Threading.Tasks;
using Library.EventBus;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventSender
    {
        Task<TEvent> SendEventAsync<TEvent, TData>(string topic, TData data,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new()
            where TData : class;

        Task<TEvent> SendEventWithoutDataAsync<TEvent>(string topic,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new();

        Task<TEvent> SendEventInTransactionAsync<TEvent, TData>(string topic, TData data,
            Guid transactionId,
            CancellationToken cancellationToken = default)
            where TEvent : Event, new()
            where TData : class;
    }
}