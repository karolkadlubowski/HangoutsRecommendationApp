using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public class GetFileByNameQueryHandler : IRequestHandler<GetFileByNameQuery, GetFileByNameResponse>
    {
        private readonly IReadOnlyFileService _fileService;

        public GetFileByNameQueryHandler(IReadOnlyFileService fileService)
            => _fileService = fileService;

        public async Task<GetFileByNameResponse> Handle(GetFileByNameQuery request, CancellationToken cancellationToken)
            => new GetFileByNameResponse
            {
                File = await _fileService.GetFileByNameAsync(request)
            };
    }
}