using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenueReview.API.Application.Features;
using VenueReview.API.Application.Features.AddVenueReview;
using VenueReview.API.Application.Features.DeleteVenueReview;

namespace VenueReview.API.Controllers
{
    /// <summary>
    /// Controller which provides VenueReview CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class VenueReviewController : BaseApiController
    {
        public VenueReviewController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return venue reviews from the database, find by the venueId
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetVenueReviewsQuery), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetVenueReviewsQuery), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetVenueReviewsQuery), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(GetVenueReviewsQuery), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVenueReviews([FromQuery] GetVenueReviewsQuery query)
        {
            _logger.Info($"Sending query {nameof(GetVenueReviewsQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Add VenueReview to the database
        /// </summary>
        /// <param name="command">
        /// Rating - must have value between 0 and 5
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddVenueReview(AddVenueReviewCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddVenueReviewCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Delete VenueReview from the database
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteVenueReviewResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DeleteVenueReviewResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(DeleteVenueReviewResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(DeleteVenueReviewResponse), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVenueReview([FromQuery] DeleteVenueReviewCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteVenueReviewCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}