using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueList.API.Application.Features.AddVenue;

namespace VenueList.API.Controllers
{
    /// <summary>
    /// Controller which provides VenueList CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/List/Favorites")]
    public class VenueController  : BaseApiController
    {
        public VenueController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }
        
        /// <summary>
        /// Add Venue to the database
        /// </summary>
        /// <param name="command">
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddVenueResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddVenueResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddVenueResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddVenueResponse), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddVenue(AddVenueCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddVenueCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}