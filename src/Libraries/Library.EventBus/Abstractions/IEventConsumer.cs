﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Library.EventBus.Abstractions
{
    public interface IEventConsumer
    {
        event EventHandler<EventType> EventReceived;

        Task ConsumeFromLatestAsync(string topic, CancellationToken cancellationToken = default);
    }
}