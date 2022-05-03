using System.Collections.Generic;
using FileStorage.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson.Serialization.Attributes;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    [BsonCollection("Folders")]
    public class FolderPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        public string FolderId { get; set; }

        public string Key { get; set; }

        public ICollection<FilePersistenceModel> Files { get; set; } = new HashSet<FilePersistenceModel>();
    }
}