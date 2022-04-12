using MediatR;

namespace FileStorage.API.Application.Features.GetBranchByName
{
    public record GetBranchByNameQuery : IRequest<GetBranchByNameResponse>
    {
        public string Name { get; init; }
    }
}