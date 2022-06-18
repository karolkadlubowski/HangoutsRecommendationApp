using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueList.API.Application.Features.AddFavorite;

namespace VenueList.API.Controllers
{
    /// <summary>
    /// Controller which provides VenueList CRUD functionality
    /// </summary>
    [ApiController]
    [Route("api/v1/List/Favorites")]
    public class FavoriteController  : BaseApiController
    {
        public FavoriteController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }
        
        /// <summary>
        /// Add Venue to the database
        /// </summary>
        /// <param name="command">
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddVenue(AddFavoriteCommand command)
        {
            _logger.Info($"Sending command: {nameof(AddFavoriteCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}