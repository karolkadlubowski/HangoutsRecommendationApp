using Category.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace Category.API.Domain.Entities
{
    public class Category : RootEntity
    {
        public string CategoryId { get; protected set; }
        public string Name { get; protected set; }

        public static Category Create(string name)
            => new Category
            {
                Name = new CategoryName(name)
            };
    }
}