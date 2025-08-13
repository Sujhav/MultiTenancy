using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace MultiTenancy.Middlewares
{
    public class ErrorHandlingMiddlewares
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlingMiddlewares(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExvceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExvceptionAsync(HttpContext httpContext, Exception ex)
        {
            var errorCode = HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails()
            {
                Status = (int)errorCode,
                Title = "An unexpected error occured",
                Detail = ex.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)errorCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);


            //var code = HttpStatusCode.InternalServerError;
            //string result;
            //if (ex is NullReferenceException)
            //{
            //    code = HttpStatusCode.BadRequest;
            //    result = JsonSerializer.Serialize(new { error = "A null reference occurred. Please check your data." });
            //}
            //else if (ex is NotImplementedException)
            //{
            //    code = HttpStatusCode.NotImplemented;
            //    result = JsonSerializer.Serialize(new { error = "the request method is not implmented" });
            //}
            //else
            //{
            //    result = JsonSerializer.Serialize(new { error = "an unknown error occurred" });
            //}
            //httpContext.Response.ContentType = "application/json";
            //httpContext.Response.StatusCode = (int)code;
            //return httpContext.Response.WriteAsync(result);
        }
    }
}
