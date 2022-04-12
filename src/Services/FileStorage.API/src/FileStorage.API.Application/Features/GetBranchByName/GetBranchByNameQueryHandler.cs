using System.Threading;
using System.Threading.Tasks;
using FileStorage.API.Application.Abstractions;
using MediatR;

namespace FileStorage.API.Application.Features.GetBranchByName
{
    public class GetBranchByNameQueryHandler : IRequestHandler<GetBranchByNameQuery, GetBranchByNameResponse>
    {
        private readonly IReadOnlyBranchService _branchService;

        public GetBranchByNameQueryHandler(IReadOnlyBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<GetBranchByNameResponse> Handle(GetBranchByNameQuery request, CancellationToken cancellationToken)
            => new GetBranchByNameResponse
            {
                Branch = await _branchService.GetBranchByNameAsync(request)
            };
    }
}