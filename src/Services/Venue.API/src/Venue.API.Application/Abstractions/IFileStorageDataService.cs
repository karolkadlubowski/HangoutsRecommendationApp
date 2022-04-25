using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared.Models.FileStorage.Dtos;
using Microsoft.AspNetCore.Http;

namespace Venue.API.Application.Abstractions
{
    public interface IFileStorageDataService
    {
        Task<IReadOnlyList<FileDto>> UploadPhotosAsync(ICollection<IFormFile> photos, long venueId);
        Task DeletePhotosFolderAsync(long venueId);
    }
}