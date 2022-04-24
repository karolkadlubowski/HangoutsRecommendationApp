using System;
using System.Collections.Generic;
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

        public async Task<IReadOnlyList<FileDto>> UploadPhotosAsync(ICollection<IFormFile> photos, long venueId)
        {
            var uploadedFiles = new List<FileDto>();

            foreach (var photo in photos)
                await UploadPhotoAsync(photo, venueId, uploadedFiles);

            return uploadedFiles;
        }

        private async Task UploadPhotoAsync(IFormFile photo, long venueId, List<FileDto> uploadedFiles)
        {
            try
            {
                _logger.Info($">> Sending request to the FileStorage API: '{_restClientConfig.BaseApiUrl}'. Request: {nameof(PutFileRequest)}");

                var request = RestRequestAbstractFactory.PutFileRequest(new PutFileRequest
                {
                    FolderKey = CreateFolderKey(venueId),
                    File = photo
                });
                var response = await _restClient.ExecuteAsync<PutFileResponse>(
                    request);
                var putFileResponse = response?.Content?.FromJSON<PutFileResponse>(JsonOptions.JsonSerializerOptions);

                _logger.Trace($"Response from the FileStorage API: {putFileResponse?.ToJSON()}");

                if (putFileResponse is not null && putFileResponse.IsSucceeded)
                {
                    _logger.Info($"<< Response from the FileStorage API is successful. File #{putFileResponse.File.FileId} uploaded to the storage");
                    uploadedFiles.Add(putFileResponse.File);
                }

                _logger.Warning($"Uploading photos to the FileStorage API failed. Message: {putFileResponse.Error?.Message}");

                throw new ServerException($"Uploading photos for the venue #{venueId} failed");
            }
            catch (Exception e)
            {
                _logger.Warning($"Rollback operation. Uploading photos for the venue #{venueId} failed due to: {e.Message}");
                //TODO remove created folder from the storage
                throw;
            }
        }

        private static string CreateFolderKey(long venueId) => $"VENUES/{venueId}";
    }
}