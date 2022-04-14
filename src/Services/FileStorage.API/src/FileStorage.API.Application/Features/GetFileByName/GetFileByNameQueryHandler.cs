using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using Library.Shared.Models.FileStorage.Dtos;
using MediatR;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public class GetFileByNameQueryHandler : IRequestHandler<GetFileByNameQuery, GetFileByNameResponse>
    {
        private readonly IReadOnlyFileService _fileService;
        private readonly IMapper _mapper;

        public GetFileByNameQueryHandler(IReadOnlyFileService fileService,
            IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<GetFileByNameResponse> Handle(GetFileByNameQuery request, CancellationToken cancellationToken)
            => new GetFileByNameResponse { File = _mapper.Map<FileDto>(await _fileService.GetFileByNameAsync(request)) };
    }
}