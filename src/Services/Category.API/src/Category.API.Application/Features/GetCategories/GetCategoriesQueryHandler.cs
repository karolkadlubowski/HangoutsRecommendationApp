using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Category.API.Application.Abstractions;
using Library.Shared.Models.Category.Dtos;
using MediatR;

namespace Category.API.Application.Features.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResponse>
    {
        private readonly IReadOnlyCategoryService _categoryService;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IReadOnlyCategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<GetCategoriesResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            => new GetCategoriesResponse
            {
                Categories = _mapper.Map<IReadOnlyList<CategoryDto>>(await _categoryService.GetCategoriesAsync())
            };
    }
}