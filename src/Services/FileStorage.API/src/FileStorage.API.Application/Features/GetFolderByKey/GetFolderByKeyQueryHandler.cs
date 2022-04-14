using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.API.Application.Abstractions;
using Library.Shared.Models.FileStorage.Dtos;
using MediatR;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public class GetFolderByKeyQueryHandler : IRequestHandler<GetFolderByKeyQuery, GetFolderByKeyResponse>
    {
        private readonly IReadOnlyFolderService _folderService;
        private readonly IMapper _mapper;

        public GetFolderByKeyQueryHandler(IReadOnlyFolderService folderService,
            IMapper mapper)
        {
            _folderService = folderService;
            _mapper = mapper;
        }

        public async Task<GetFolderByKeyResponse> Handle(GetFolderByKeyQuery request, CancellationToken cancellationToken)
            => new GetFolderByKeyResponse { Folder = _mapper.Map<FolderDto>(await _folderService.GetFolderByKeyAsync(request)) };
    }
}