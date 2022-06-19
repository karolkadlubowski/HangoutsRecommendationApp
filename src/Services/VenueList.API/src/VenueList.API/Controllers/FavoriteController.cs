﻿using System.Net;
using System.Threading.Tasks;
using Library.Shared.Controllers;
using Library.Shared.Extensions;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VenueList.API.Application.Features.AddFavorite;
using VenueList.API.Application.Features.DeleteFavorite;
using VenueList.API.Application.Features.GetFavorites;

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
        /// Return Favorite entities from the database using pagination
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetFavoritesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetFavoritesResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetVenues([FromQuery] GetFavoritesQuery query)
        {
            _logger.Info($"Sending query: {nameof(GetFavoritesQuery)}");

            var response = await _mediator.Send(query);

            return this.CreateResponse(response);
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
        
        /// <summary>
        /// Delete Favorite from the database
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(typeof(AddFavoriteResponse), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteFavorite([FromQuery] DeleteFavoriteCommand command)
        {
            _logger.Info($"Sending command: {nameof(DeleteFavoriteCommand)}");

            var response = await _mediator.Send(command);

            return this.CreateResponse(response);
        }
    }
}