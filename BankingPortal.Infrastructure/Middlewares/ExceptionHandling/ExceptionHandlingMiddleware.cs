using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BankingPortal.Infrastructure.Extensions.Middlewares.ExecutionContext;
using System.Net;
using BankingPortal.Shared.Models;

namespace BankingPortal.Infrastructure.Extensions.Middlewares.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExecutionRequestContext _executionRequestContext;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IExecutionRequestContext executionRequestContext)
        {
            _next = next;
            _logger = logger;
            _executionRequestContext = executionRequestContext;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                UserCreationException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;
            var response = new ResponseModel<object>
            {
                IsSuccess = false,
                Status = "An error occurred , please contact with administrator",
                Data = null,
                Errors = new List<string> { exception.Message },
                TrackingCorrelationId = _executionRequestContext.TrackingCorrelationId,
                Timestamp = DateTime.UtcNow
            };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
