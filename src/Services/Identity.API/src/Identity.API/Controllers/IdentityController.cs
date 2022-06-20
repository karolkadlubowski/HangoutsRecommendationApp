using System.Threading.Tasks;
using Identity.API.Application.Features.ChangeUserEmail;
using Identity.API.Application.Features.ChangeUserPassword;
using Identity.API.Application.Features.SigninUser;
using Identity.API.Application.Features.SignupUser;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("signin")]
        public async Task<IActionResult> SigninUser(SignInUserCommand command)
        {
            _logger.Info($"Sending command: {nameof(SignInUserCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> SignupUser(SignupUserCommand command)
        {
            _logger.Info($"Sending command: {nameof(SignupUserCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        [Authorize]
        [HttpPatch("change/password")]
        public async Task<IActionResult> ChangeUserPassword(ChangeUserPasswordCommand command)
        {
            _logger.Info($"Sending command: {nameof(ChangeUserPasswordCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        [Authorize]
        [HttpPatch("change/email")]
        public async Task<IActionResult> ChangeUserEmail(ChangeUserEmailCommand command)
        {
            _logger.Info($"Sending command: {nameof(ChangeUserEmailCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}