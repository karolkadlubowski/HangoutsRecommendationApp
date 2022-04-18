using System.Linq;

namespace Category.API.Tests.Unit.Utilities.Factories
{
    public static class StringFactory
    {
        public static string CreateStringWithLength(int length, char character)
            => string.Join(string.Empty, Enumerable.Repeat(character, length));
    }
}