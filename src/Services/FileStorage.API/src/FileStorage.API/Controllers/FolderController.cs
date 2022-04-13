using System.Net;
using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFolderByKey;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    /// <summary>
    /// Controller which provides Folder CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FolderController : BaseApiController
    {
        public FolderController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return Folder entity from the database, find by the folder key
        /// </summary>
        /// <param name="query">
        /// Key - cannot be null or empty
        /// </param>
        [HttpGet]
        [ProducesResponseType(typeof(GetFolderByKeyResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetFolderByKeyResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetFolderByKeyResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(GetFolderByKeyResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFolderByKey([FromQuery] GetFolderByKeyQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetFolderByKeyQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }
    }
}