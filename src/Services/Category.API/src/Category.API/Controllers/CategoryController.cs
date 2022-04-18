using System.Net;
using System.Threading.Tasks;
using Category.API.Application.Features.AddCategory;
using Category.API.Application.Features.GetCategories;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Category.API.Controllers
{
    /// <summary>
    /// Controller which provides Category CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : BaseApiController
    {
        public CategoryController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Returns all Category entities from the database
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(GetCategoriesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetCategoriesResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetCategoriesQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Add Category to the database if duplicate does not already exist
        /// </summary>
        /// <param name="command">
        /// Name - cannot be null or empty, cannot exceed 50 characters
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddCategoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddCategoryResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddCategoryResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddCategoryResponse), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddCategory(AddCategoryCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddCategoryCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}