using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.PutFile
{
    public class PutFileCommandHandler : IRequestHandler<PutFileCommand, PutFileResponse>
    {
        private readonly IFileService _fileService;

        public PutFileCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<PutFileResponse> Handle(PutFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileService.PutFileAsync(request);

            return new PutFileResponse { File = file };
        }
    }
}