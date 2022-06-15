using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Library.Shared.AppConfigs;
using Library.Shared.Clients.Abstractions;
using Library.Shared.Clients.Factories;
using Library.Shared.DI;
using Library.Shared.Exceptions;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Options;
using Microsoft.AspNetCore.Http;
using Venue.API.Application.Abstractions;
using Venue.API.Application.Providers;
using Venue.API.Domain.ValueObjects;
using Venue.API.Infrastructure.Clients.Factories;
using Venue.API.Infrastructure.Services.Requests.Factories;
using Venue.API.Infrastructure.Services.Requests.FileStorageApi;
using Venue.API.Infrastructure.Services.Responses.FileStorageApi;

namespace Venue.API.Infrastructure.Services
{
    public class FileStorageDataService : IFileStorageDataService
    {
        private readonly IRestClient _restClient;
        private readonly ILogger _logger;

        private readonly RestClientConfig _restClientConfig;

        public FileStorageDataService(IDIProvider diProvider,
            IConfigurationProvider configurationProvider,
            ILogger logger)
        {
            _restClientConfig = configurationProvider.GetConfiguration().RestClientsConfig.FileStorageApi;
            _restClient = diProvider.ResolveServiceWhere<IRestClientFactory, FileStorageRestClientFactory>()
                .CreateRestClient(_restClientConfig.BaseApiUrl);
            _logger = logger;
        }

        public async Task<IReadOnlyList<FileDto>> GetPhotosFromFolderAsync(long venueId)
        {
            try
            {
                _logger.Info($">> Sending request to the FileStorage API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(GetFolderRequest)}");

                var response = await _restClient.ExecuteAsync<GetFolderResponse>(RestRequestAbstractFactory.GetFolderRequest(new GetFolderRequest
                {
                    FolderKey = new PhotosFolderKey(venueId)
                }));

                _logger.Trace($"Response {nameof(GetFolderResponse)} from the FileStorage API: {response.Content}");

                var getFolderResponse = response.Content?.FromJSON<GetFolderResponse>(JsonOptions.JsonSerializerOptions);

                if (getFolderResponse is not null && getFolderResponse.IsSucceeded)
                {
                    var photos = getFolderResponse.Folder.Files;
                    _logger.Info($"Response {nameof(GetFolderResponse)} from the FileStorage API is successful. Returning {photos.Count} photos");

                    return photos.ToList();
                }

                _logger.Info($"Venue #{venueId} has not any photos in the file storage");
                return ImmutableList<FileDto>.Empty;
            }
            catch (Exception e)
            {
                _logger.Warning($"Photos for venue #{venueId} cannot be loaded due to: {e.Message}");
                return ImmutableList<FileDto>.Empty;
            }
        }

        public async Task<IReadOnlyList<FileDto>> UploadPhotosAsync(ICollection<IFormFile> photos, long venueId)
        {
            var uploadedFiles = new List<FileDto>();

            foreach (var photo in photos)
                await UploadPhotoAsync(photo, venueId, uploadedFiles);

            return uploadedFiles;
        }

        public async Task DeletePhotosFolderAsync(long venueId)
        {
            try
            {
                _logger.Info($">> Sending request to the FileStorage API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(DeleteFolderRequest)}");

                var response = await _restClient.ExecuteAsync<DeleteFolderResponse>(RestRequestAbstractFactory.DeleteFolderRequest(new DeleteFolderRequest
                {
                    FolderKey = new PhotosFolderKey(venueId)
                }));

                _logger.Trace($"Response {nameof(DeleteFolderResponse)} from the FileStorage API: {response.Content}");

                var deleteFolderResponse = response.Content?.FromJSON<DeleteFolderResponse>(JsonOptions.JsonSerializerOptions);

                if (deleteFolderResponse is not null && deleteFolderResponse.IsSucceeded)
                    _logger.Info("Operation rollback completed. Photos deleted from the storage");
            }
            catch (Exception e)
            {
                _logger.Warning($"Photos for venue #{venueId} cannot be deleted due to: {e.Message}");
            }
        }

        private async Task UploadPhotoAsync(IFormFile photo, long venueId, List<FileDto> uploadedFiles)
        {
            try
            {
                _logger.Info($">> Sending request to the FileStorage API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(PutFileRequest)}");

                var response = await _restClient.ExecuteAsync<PutFileResponse>(
                    RestRequestAbstractFactory.PutFileRequest(new PutFileRequest
                    {
                        FolderKey = new PhotosFolderKey(venueId),
                        File = photo
                    }));
                var putFileResponse = response?.Content?.FromJSON<PutFileResponse>(JsonOptions.JsonSerializerOptions);

                _logger.Trace($"Response {nameof(PutFileResponse)} from the FileStorage API: {putFileResponse?.ToJSON()}");

                if (putFileResponse is not null && putFileResponse.IsSucceeded)
                {
                    _logger.Info($"<< Response {nameof(PutFileResponse)} from the FileStorage API is successful. File #{putFileResponse.File.FileId} uploaded to the storage");
                    uploadedFiles.Add(putFileResponse.File);
                }
                else
                {
                    _logger.Warning($"Uploading photos to the FileStorage API failed. Message: {putFileResponse.Error?.Message}");
                    throw new ServerException($"Uploading photos for the venue #{venueId} failed");
                }
            }
            catch (Exception e)
            {
                _logger.Warning($"Rollback operation. Uploading photos for the venue #{venueId} failed due to: {e.Message}");

                await DeletePhotosFolderAsync(venueId);

                throw new ServerException($"Photos for venue #{venueId} cannot be uploaded due to: {e.Message}");
            }
        }
    }
}