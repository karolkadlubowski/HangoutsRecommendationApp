using System.Collections.Generic;
using Venue.API.Domain.Entities.Models;

namespace Venue.API.Tests.Unit.Utilities.Factories
{
    public static class PhotosFactory
    {
        public static IEnumerable<Photo> Prepare(int count, int offset = default)
        {
            for (var i = offset; i < count + offset; i++)
                yield return new Photo { Key = $"{i + 1}" };
        }
    }
}