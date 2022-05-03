using System;
using System.Text;
using System.Threading.Tasks;
using Library.Shared.Constants;
using Library.Shared.Dictionaries;
using Library.Shared.Models.Response;
using Microsoft.AspNetCore.Http;

namespace Library.Shared.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteErrorResponseAsync(this HttpContext context, Exception e, string errorCode)
        {
            var response = new BaseResponse(new Error(errorCode, e.Message));
            var jsonResponse = response.ToJSON();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ErrorStatusCodeDictionary.GetStatusCode(errorCode);
            context.Response.ContentLength = Encoding.UTF8.GetBytes(jsonResponse).Length;

            context.Response.AddApplicationError(e.Message);

            await context.Response.WriteAsync(jsonResponse).ConfigureAwait(false);
        }

        public static void AddApplicationError(this HttpResponse response, string errorMessage)
        {
            response.Headers.Add(Headers.ApplicationError, errorMessage);
            response.AddAccessControlExposeHeaders(Headers.ApplicationError);
            response.Headers.Add(Headers.AccessControlAllowOrigin, "*");
        }

        public static void AddAccessControlExposeHeaders(this HttpResponse response, params string[] headers)
            => response.Headers.Add(Headers.AccessControlExposeHeaders, string.Join(", ", headers));
    }
}