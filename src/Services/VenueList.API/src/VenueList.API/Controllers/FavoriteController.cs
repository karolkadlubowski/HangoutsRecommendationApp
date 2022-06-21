using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenueList.API.Application.Features.AddVenueToFavorites;
using VenueList.API.Application.Features.DeleteFavorite;
using VenueList.API.Application.Features.GetUserFavorites;

namespace VenueList.API.Controllers
{
    /// <summary>
    /// Controller which provides VenueList CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/List/Favorites")]
    [Authorize]
    public class FavoriteController : BaseApiController
    {
        public FavoriteController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        /// <summary>
        /// Return favorite user's venues from the database using pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetUserFavoritesResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetUserFavoritesResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVenues([FromQuery] GetUserFavoritesQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetUserFavoritesQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Add venue to user's favorites
        /// </summary>
        /// <param name="command">
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddVenue(AddVenueToFavoritesCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddVenueToFavoritesCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Delete favorite venue from user's list
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddVenueToFavoritesResponse), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteFavorite([FromQuery] DeleteFavoriteCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteFavoriteCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}