using BankApp_Api.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace BankApp_Api.Middleware
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleWare> _logger;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        // This method is called for each HTTP request and is responsible for handling exceptions
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // İf the request is successful, pass it to the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response type to JSON
            context.Response.ContentType = "application/json";

            // Set the response status code to 400 Bad Request
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = ApiResponse<object>.FailureResult(exception.Message);

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }









    }
}
