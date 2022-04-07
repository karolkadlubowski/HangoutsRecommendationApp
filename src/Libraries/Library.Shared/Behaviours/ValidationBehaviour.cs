using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Library.Shared.Constants;
using Library.Shared.Logging;
using Library.Shared.Models.Response;
using Library.Shared.Utils;
using MediatR;
using ValidationException = Library.Shared.Exceptions.ValidationException;

namespace Library.Shared.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : BaseApiResponse
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(ILogger logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (_validators.Any())
                {
                    var context = new ValidationContext<TRequest>(request);

                    var validationResults =
                        await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                    var failures = validationResults.SelectMany(r => r.Errors)
                        .Where(f => f != null)
                        .ToList();

                    if (failures.Count != 0)
                        throw new ValidationException(failures);
                }
            }
            catch (ValidationException e)
            {
                return BehaviourUtils<TRequest, TResponse>.HandleValidationException(e, ErrorCodes.ValidationFailed,
                    _logger);
            }
            catch (Exception e)
            {
                return BehaviourUtils<TRequest, TResponse>.HandleException(e, ErrorCodes.UnhandledException, _logger);
            }

            return await next();
        }
    }
}