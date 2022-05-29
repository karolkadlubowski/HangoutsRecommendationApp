using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueReview.API.Application.Features;

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
        
    }
}