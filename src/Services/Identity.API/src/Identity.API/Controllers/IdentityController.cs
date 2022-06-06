using System.Threading.Tasks;
using Identity.API.Application.Features.SignupUser;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    /// <summary>
    /// Controller which provides Identity functionalities
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class IdentityController : BaseApiController
    {
        public IdentityController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignupUser(SignupUserCommand command)
        {
            _logger.Info($"Sending command: {nameof(SignupUserCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}