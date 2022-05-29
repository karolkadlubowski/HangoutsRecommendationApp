using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueReview.API.Application.Features;
using VenueReview.API.Application.Features.AddVenueReview;

namespace VenueReview.API.Controllers
{
    /// <summary>
    /// Controller which provides Folder CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VenueReviewController : BaseApiController
    {
        public VenueReviewController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return venue reviews from the database, find by the venue Id
        /// </summary>
        /// <param name="query">
        /// Id - cannot be null or empty
        /// </param>
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
        /// VenueId - have to be long
        /// Content - have to be string
        /// CreatorId - have to be long
        /// Rataing - have to be double between 0 - 5
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddVenueReviewResponse), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddVenueReview(AddVenueReviewCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddVenueReviewCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}