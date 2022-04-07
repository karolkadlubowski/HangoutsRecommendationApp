using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Shared.Constants;
using Library.Shared.Exceptions;
using Library.Shared.Logging;
using Library.Shared.Models.Response;
using Library.Shared.Utils;
using MediatR;

namespace Library.Shared.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : BaseApiResponse
    {
        private readonly ILogger _logger;

        public UnhandledExceptionBehaviour(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (BaseException e)
            {
                return BehaviourUtils<TRequest, TResponse>.HandleException(e, e.ErrorCode, _logger);
            }
            catch (Exception e)
            {
                return BehaviourUtils<TRequest, TResponse>.HandleException(e, ErrorCodes.UnhandledException, _logger);
            }
        }
    }
}