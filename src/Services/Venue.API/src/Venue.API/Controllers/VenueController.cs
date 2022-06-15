using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Venue.API.Application.Features.CreateVenue;
using Venue.API.Application.Features.DeleteVenue;
using Venue.API.Application.Features.GetVenue;
using Venue.API.Application.Features.GetVenues;
using Venue.API.Application.Features.UpdateVenue;

namespace Venue.API.Controllers
{
    /// <summary>
    /// Controller which provides Venue CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VenueController : BaseApiController
    {
        public VenueController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return Venue entity from the database. Append photos from the FileStorage API
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetVenueResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetVenueResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVenues([FromQuery] GetVenueQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetVenueQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Return Venue entities from the database using pagination
        /// </summary>
        [HttpGet("list")]
        [ProducesResponseType(typeof(GetVenuesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetVenuesResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVenues([FromQuery] GetVenuesQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetVenuesQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Create new Venue entity in the database
        /// </summary>
        /// <param name="command">
        /// Name - cannot be null or empty, cannot exceed 200 characters
        /// Description - cannot exceed 2000 characters
        /// CategoryName - cannot be null or empty
        /// Address - cannot be null or empty, cannot exceed 500 characters
        /// Photos - cannot contain more than 6 photos
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateVenueResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CreateVenueResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(CreateVenueResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(CreateVenueResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateVenue([FromForm] CreateVenueCommand command)
        {
            _logger.Info($"Sending command: {nameof(CreateVenueCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Update existing Venue entity found in the database by the VenueId
        /// </summary>
        /// <param name="command">
        /// Name - cannot be null or empty, cannot exceed 200 characters
        /// Description - cannot exceed 2000 characters
        /// CategoryName - cannot be null or empty
        /// Address - cannot be null or empty, cannot exceed 500 characters
        /// </param>
        [HttpPut]
        [ProducesResponseType(typeof(UpdateVenueResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UpdateVenueResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UpdateVenueResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(UpdateVenueResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateVenue(UpdateVenueCommand command)
        {
            _logger.Info($"Sending command: {nameof(UpdateVenueCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Delete existing Venue entity from the database
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteVenueResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DeleteVenueResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(DeleteVenueResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVenue([FromQuery] DeleteVenueCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteVenueCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}