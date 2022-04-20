using Library.Shared.Controllers;
using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}