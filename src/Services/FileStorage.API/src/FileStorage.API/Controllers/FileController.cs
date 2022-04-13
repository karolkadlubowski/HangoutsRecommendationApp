using System.Net;
using System.Threading.Tasks;
using FileStorage.API.Application.Features.PutFile;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    /// <summary>
    /// Controller which provides File CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/file")]
    public class FileController : BaseApiController
    {
        public FileController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Upsert File entity into the database and upload file to the server storage
        /// </summary>
        /// <param name="command">
        /// Key - cannot be null or empty
        /// Name - cannot be null or empty, cannot exceed 60 characters
        /// File - cannot be null
        /// </param>
        [HttpPut]
        [ProducesResponseType(typeof(PutFileResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PutFileResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(PutFileResponse), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> PutFile([FromForm] PutFileCommand command)
        {
            _logger.Info($"Sending command: {nameof(PutFileCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}