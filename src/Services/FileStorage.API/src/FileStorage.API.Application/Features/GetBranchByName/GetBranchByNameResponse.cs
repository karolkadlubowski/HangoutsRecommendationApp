using Library.Shared.Models.FileStorage.Dtos;
using Library.Shared.Models.Response;

namespace FileStorage.API.Application.Features.GetBranchByName
{
    public record GetBranchByNameResponse : BaseApiResponse
    {
        public BranchDto Branch { get; init; }

        public GetBranchByNameResponse(Error error = null) : base(error)
        {
        }
    }
}