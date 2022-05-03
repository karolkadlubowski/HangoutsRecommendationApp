using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserProfile.API.Application.Features.GetUserProfileQuery;

namespace UserProfile.API.Controllers
{
    /// <summary>
    /// Controller which provides UserProfile CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserProfileController : BaseApiController
    {
        public UserProfileController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Returns UserProfile at given ID from database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetUserProfileResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetUserProfileResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetUserProfileResponse), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserProfile([FromQuery] GetUserProfileQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetUserProfileQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }
    }
}