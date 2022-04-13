using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public class GetFolderByKeyQueryHandler : IRequestHandler<GetFolderByKeyQuery, GetFolderByKeyResponse>
    {
        private readonly IReadOnlyFolderInformationService _folderInformationService;

        public GetFolderByKeyQueryHandler(IReadOnlyFolderInformationService folderInformationService)
        {
            _folderInformationService = folderInformationService;
        }

        public async Task<GetFolderByKeyResponse> Handle(GetFolderByKeyQuery request, CancellationToken cancellationToken)
            => new GetFolderByKeyResponse
            {
                FolderInformation = await _folderInformationService.GetFolderByKeyAsync(request)
            };
    }
}