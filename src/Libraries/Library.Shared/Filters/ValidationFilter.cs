using System.Linq;
using System.Net;
using Library.Shared.Constants;
using Library.Shared.Exceptions;
using Library.Shared.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Shared.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .ToDictionary(kv => kv.Key,
                        e => e.Value.Errors
                            .Select(me => me.ErrorMessage));

                var validationResponse = new BaseApiResponse(new Error(
                    ErrorCodes.ValidationFailed,
                    ValidationException.CustomMessage,
                    HttpStatusCode.UnprocessableEntity,
                    errors
                ));

                context.Result = new JsonResult(validationResponse) { StatusCode = (int)HttpStatusCode.UnprocessableEntity };
            }
        }
    }
}