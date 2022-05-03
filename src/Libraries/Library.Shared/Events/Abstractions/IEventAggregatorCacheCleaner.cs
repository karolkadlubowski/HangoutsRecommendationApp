using System;
using System.Collections.Generic;

namespace Library.Shared.Events.Abstractions
{
    public interface IEventAggregatorCacheCleaner
    {
        IReadOnlyList<Guid> CleanUpCache(int cacheExpirationTimeInMinutes);
    }
}