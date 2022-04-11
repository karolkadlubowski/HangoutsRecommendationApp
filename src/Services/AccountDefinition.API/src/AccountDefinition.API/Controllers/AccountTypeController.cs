using System.Net;
using System.Threading.Tasks;
using AccountDefinition.API.Application.Features.GetAccountTypes;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountDefinition.API.Controllers
{
    /// <summary>
    /// Controller which provides AccountType readonly functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountTypeController : BaseApiController
    {
        public AccountTypeController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Returns all AccountType entities from the database
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(GetAccountTypesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetAccountTypesResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAccountTypes([FromQuery] GetAccountTypesQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetAccountTypesQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }
    }
}