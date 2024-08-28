using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using BankingPortal.Infrastructure.Extensions.Middlewares.ExceptionHandling;
using BankingPortal.Infrastructure.Extensions.Middlewares.ExecutionContext;
using BankingPortal.Infrastructure.Extensions.Middlewares.Logging;

namespace BankingPortal.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            return app;
        }

        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            return app;
        }

        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggingMiddleware>(); 

            return app;
        }
        public static IApplicationBuilder UseRequestExecutionContext(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExecutionRequestContextMiddleware>();
            return app;
        }
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
