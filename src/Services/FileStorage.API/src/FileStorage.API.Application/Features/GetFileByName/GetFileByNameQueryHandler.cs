using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using FileStorage.API.Application.Factories;
using MediatR;
using SimpleFileSystem.Abstractions;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public class GetFileByNameQueryHandler : IRequestHandler<GetFileByNameQuery, GetFileByNameResponse>
    {
        private readonly IReadOnlyFileService _fileService;
        private readonly IFileSystemConfiguration _fileSystemConfiguration;

        public GetFileByNameQueryHandler(IReadOnlyFileService fileService,
            IFileSystemConfiguration fileSystemConfiguration)
        {
            _fileService = fileService;
            _fileSystemConfiguration = fileSystemConfiguration;
        }

        public async Task<GetFileByNameResponse> Handle(GetFileByNameQuery request, CancellationToken cancellationToken)
        {
            var file = await _fileService.GetFileByNameAsync(request);

            return new GetFileByNameResponse
            {
                File = file with
                {
                    FileUrl = FileUrlFactory.CombineUrl(_fileSystemConfiguration,
                        file.Key)
                }
            };
        }
    }
}