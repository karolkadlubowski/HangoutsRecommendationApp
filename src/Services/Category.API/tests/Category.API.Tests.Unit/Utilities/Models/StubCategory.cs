using System;

namespace Category.API.Tests.Unit.Utilities.Models
{
    public class StubCategory : API.Domain.Entities.Category
    {
        public StubCategory(string categoryId,
            string name,
            DateTime createdOn)
        {
            CategoryId = categoryId;
            Name = name;
            CreatedOn = createdOn;
        }
    }
}