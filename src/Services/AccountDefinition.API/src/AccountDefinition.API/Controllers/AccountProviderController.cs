using System.Net;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Features.AddAccountProvider;
using AccountDefinition.API.Application.Features.DeleteAccountProviderById;
using AccountDefinition.API.Application.Features.GetAccountProviders;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountDefinition.API.Controllers
{
    /// <summary>
    /// Controller which provides AccountProvider CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountProviderController : BaseApiController
    {
        public AccountProviderController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Returns all AccountProvider entities from the database
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(GetAccountProvidersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetAccountProvidersResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAccountProviders([FromQuery] GetAccountProvidersQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetAccountProvidersQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Add new AccountProvider entity to the database
        /// </summary>
        /// <param name="command">
        /// Provider - cannot be null or empty
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddAccountProviderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddAccountProviderResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddAccountProviderResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddAccountProviderResponse), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddAccountProvider(AddAccountProviderCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddAccountProviderCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Delete AccountProvider entity from the database with specified AccountProviderId
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteAccountProviderByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DeleteAccountProviderByIdResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(DeleteAccountProviderByIdResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAccountProviderById([FromQuery] DeleteAccountProviderByIdCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteAccountProviderByIdCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}