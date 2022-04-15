using System.Collections.Generic;
using FileStorage.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson.Serialization.Attributes;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    [BsonCollection("Folders")]
    public record FolderPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        public string FolderId { get; init; }

        public string Key { get; init; }

        public ICollection<FilePersistenceModel> Files { get; init; } = new HashSet<FilePersistenceModel>();
    }
}