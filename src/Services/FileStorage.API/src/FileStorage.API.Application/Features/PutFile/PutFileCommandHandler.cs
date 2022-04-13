using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.PutFile
{
    public class PutFileCommandHandler : IRequestHandler<PutFileCommand, PutFileResponse>
    {
        private readonly IFileInformationService _fileInformationService;

        public PutFileCommandHandler(IFileInformationService fileInformationService)
        {
            _fileInformationService = fileInformationService;
        }

        public async Task<PutFileResponse> Handle(PutFileCommand request, CancellationToken cancellationToken)
        {
            var fileInformation = await _fileInformationService.PutFileInformationAsync(request);

            return new PutFileResponse { FileInformation = fileInformation };
        }
    }
}