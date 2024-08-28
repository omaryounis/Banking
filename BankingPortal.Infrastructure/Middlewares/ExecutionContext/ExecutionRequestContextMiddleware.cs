using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BankingPortal.Infrastructure.Extensions.Attributes;
using BankingPortal.Infrastructure.Extensions.Helpers;
using System.Text;

namespace BankingPortal.Infrastructure.Extensions.Middlewares.ExecutionContext
{
    public class ExecutionRequestContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly string[] _excludedPaths = ["/cap-dashboard"];
        private readonly ILogger<ExecutionRequestContextMiddleware> _logger;

        public ExecutionRequestContextMiddleware(RequestDelegate next, ILogger<ExecutionRequestContextMiddleware> logger, JwtTokenHelper jwtTokenHelper)
        {
            _next = next;
            _logger = logger;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task InvokeAsync(HttpContext context, IExecutionRequestContext requestContext)
        {
            var endpoint = context.GetEndpoint();
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if(authHeader != null) {
                if(!authHeader.Split(" ").First().Contains("Basic"))
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (token != null)
                    {
                        var principal = _jwtTokenHelper.ValidateToken(token);
                        if (principal != null)
                        {
                            requestContext.UserId = int.Parse(principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value);

                        }
                    }
                }
            }
        
            if (_excludedPaths.Any(path => context.Request.Path.StartsWithSegments(path))
                || endpoint?.Metadata.GetMetadata<ExcludeFromExecutionContextMiddlewareAttribute>() != null
                )
            {
                requestContext.TrackingCorrelationId = Guid.NewGuid();
                await _next(context);
                return;
            }
            try
            {
                GetApplicationSecretKeyId(context, requestContext);
                await GetRequestCurl(context, requestContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in ExecutionRequestContextMiddleware");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }
            await _next(context);
        }

        private async Task GetRequestCurl(HttpContext context, IExecutionRequestContext requestContext)
        {
            // Extract request method
            var method = context.Request.Method;

            // Extract request URL
            var url = context.Request.Path + context.Request.QueryString;

            // Extract headers
            var headers = context.Request.Headers
                .Select(h => $"-H \"{h.Key}: {h.Value}\"")
                .ToList();
            // Extract body if the method allows a body
            string body = null;
            if (method == "POST" || method == "PUT" || method == "PATCH")
            {
                context.Request.EnableBuffering();
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
            } 
            // Construct the curl command
            var curlCommand = new StringBuilder();
            curlCommand.Append($"curl -X {method} \"{url}\"");

            foreach (var header in headers)
            {
                curlCommand.Append($" {header}");
            }

            if (body != null)
            {
                curlCommand.Append($" -d \"{body}\"");
            }
            // Store the curl command in the request context
            requestContext.CurlCommand = curlCommand.ToString();
        }

        private void GetApplicationSecretKeyId(HttpContext context,IExecutionRequestContext requestContext)
        {
            var applicationSecretKey = context.Request.Headers["X-App-Secret-Key"];
            if (string.IsNullOrEmpty(applicationSecretKey))
                throw new UnauthorizedAccessException("Application Secret Key is missing.");

            if (!Guid.TryParse(applicationSecretKey, out var secretKey))
            {
                throw new UnauthorizedAccessException("Invalid Application Secret Key format.");
            }
            // Set the correlationId into the context
            requestContext.ApplicationSecretKey = Guid.Parse(applicationSecretKey);

            // Add correlationId to the response header
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-App-Secret-Key", applicationSecretKey);
                return Task.CompletedTask;
            });
        }
    }
}
