﻿using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.DeleteFolder
{
    public record DeleteFolderResponse : BaseResponse
    {
        public string DeletedFolderId { get; init; }
        public string DeletedFolderKey { get; init; }

        public DeleteFolderResponse(Error error = null) : base(error)
        {
        }
    }
}