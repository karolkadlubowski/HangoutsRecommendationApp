using System.Net;
using System.Threading.Tasks;
using FileStorage.API.Application.Features.GetFileByName;
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
    [Route("api/v1/[controller]")]
    public class FileController : BaseApiController
    {
        public FileController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return File entity from the database
        /// </summary>
        /// <param name="query">
        /// FileName - cannot be null or empty
        /// FolderKey - cannot be null or empty
        /// </param>
        [HttpGet]
        [ProducesResponseType(typeof(GetFileByNameResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetFileByNameResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetFileByNameResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(GetFileByNameResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFileByName([FromQuery] GetFileByNameQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetFileByNameQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }
        
        /// <summary>
        /// Upsert File entity into the database and upload file to the server storage
        /// </summary>
        /// <param name="command">
        /// FolderKey - cannot be null or empty
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