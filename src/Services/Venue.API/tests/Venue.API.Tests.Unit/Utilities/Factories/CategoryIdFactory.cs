using System;

namespace Venue.API.Tests.Unit.Utilities.Factories
{
    public static class CategoryIdFactory
    {
        public static string CategoryId => Guid.NewGuid().ToString().Substring(0, 24);
    }
}