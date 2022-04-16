using System.Collections.Generic;
using FileStorage.API.Application.Database.PersistenceModels;
using FileStorage.API.Domain.ValueObjects;

namespace FileStorage.API.Tests.Unit.Utilities.Factories
{
    public static class FoldersFactory
    {
        public static IEnumerable<FolderPersistenceModel> PrepareSubfolders(string key, int count)
        {
            for (var i = 0; i < count; i++)
                yield return new FolderPersistenceModel
                {
                    FolderId = new GuidIdentifier(),
                    Key = key
                };
        }
    }
}