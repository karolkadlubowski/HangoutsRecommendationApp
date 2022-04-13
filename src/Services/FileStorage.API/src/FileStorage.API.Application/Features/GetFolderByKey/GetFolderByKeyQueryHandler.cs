using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public class GetFolderByKeyQueryHandler : IRequestHandler<GetFolderByKeyQuery, GetFolderByKeyResponse>
    {
        private readonly IReadOnlyFolderService _folderService;

        public GetFolderByKeyQueryHandler(IReadOnlyFolderService folderService)
        {
            _folderService = folderService;
        }

        public async Task<GetFolderByKeyResponse> Handle(GetFolderByKeyQuery request, CancellationToken cancellationToken)
            => new GetFolderByKeyResponse
            {
                Folder = await _folderService.GetFolderByKeyAsync(request)
            };
    }
}