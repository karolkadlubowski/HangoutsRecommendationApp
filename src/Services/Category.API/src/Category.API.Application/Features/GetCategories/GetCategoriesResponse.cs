﻿using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Category.Dtos;
using Library.Shared.Models.Response;

namespace Category.API.Application.Features.GetCategories
{
    public record GetCategoriesResponse : BaseResponse
    {
        public IReadOnlyList<CategoryDto> Categories { get; init; } = ImmutableList<CategoryDto>.Empty;

        public GetCategoriesResponse(Error error = null) : base(error)
        {
        }
    }
}