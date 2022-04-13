﻿using FileStorage.API.Domain.ValueObjects;
using Library.Shared.Models;

namespace FileStorage.API.Domain.Entities
{
    public class File : RootEntity
    {
        public string FileId { get; protected set; }
        public string Key { get; protected set; }
        public string Name { get; protected set; }

        public static File CreateDefault(string name, Folder folder)
        {
            var file = new File
            {
                FileId = new GuidId(),
                Name = new FileName(name)
            };

            file.Key = new FileKey(file.Name, folder);

            return file;
        }
    }
}