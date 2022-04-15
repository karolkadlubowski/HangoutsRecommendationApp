using System.Net;
using System.Threading.Tasks;
using FileStorage.API.Application.Features.DeleteFolder;
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

        /// <summary>
        /// Delete Folder entity from the database and delete folder with all their files from the server storage
        /// </summary>
        /// <param name="command">
        /// FolderKey - cannot be null or empty
        /// </param>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteFolderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DeleteFolderResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(DeleteFolderResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(DeleteFolderResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteFolder([FromQuery] DeleteFolderCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteFolderCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}