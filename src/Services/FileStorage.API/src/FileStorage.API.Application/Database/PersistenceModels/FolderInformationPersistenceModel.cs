using System.Collections.Generic;
using FileStorage.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson.Serialization.Attributes;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    [BsonCollection("Folders")]
    public record FolderInformationPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        public string FolderInformationId { get; init; }

        public string Key { get; init; }

        public ICollection<FileInformationPersistenceModel> FileInformations { get; init; } = new HashSet<FileInformationPersistenceModel>();
    }
}