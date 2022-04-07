using Library.Shared.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Shared.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger _logger;

        protected BaseApiController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}