using System.Collections.Generic;
using FileStorage.API.Application.Database.Attributes;
using Library.Database;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FileStorage.API.Application.Database.PersistenceModels
{
    [BsonCollection("Branches")]
    public record BranchPersistenceModel : BasePersistenceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BranchId { get; init; }

        public string Name { get; init; }
        public string ParentBranchId { get; init; }

        public BranchPersistenceModel ParentBranch { get; init; }

        public ICollection<FileInformationPersistenceModel> FileInformations { get; init; } = new HashSet<FileInformationPersistenceModel>();
    }
}