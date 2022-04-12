using System.Net;
using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetBranchByName;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    /// <summary>
    /// Controller which provides Branch CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BranchController : BaseApiController
    {
        public BranchController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Returns Branch entity from the database, find by the branch name
        /// </summary>
        /// <param name="query">
        /// Name - cannot be null or empty, cannot exceed 60 characters
        /// </param>
        [HttpGet]
        [ProducesResponseType(typeof(GetBranchByNameResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetBranchByNameResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetBranchByNameResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(GetBranchByNameResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBranch([FromQuery] GetBranchByNameQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetBranchByNameQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }
    }
}