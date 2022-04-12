using System;

namespace FileStorage.API.Application.Database.Attributes
{
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
            => CollectionName = collectionName;
    }
}