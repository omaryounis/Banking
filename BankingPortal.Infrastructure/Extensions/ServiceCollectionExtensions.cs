using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Serilog.Events;
using BankingPortal.Infrastructure.Extensions.Helpers;
using BankingPortal.EntityFrameWorkCore;
using Microsoft.EntityFrameworkCore;
using BankingPortal.Domain.Interfaces;
using BankingPortal.EntityFrameWorkCore.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
namespace BankingPortal.Infrastructure.Extensions
{

    public static class ServiceCollectionExtensions
    {
         
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger(configuration);
            services.AddRepositories();
            //services.AddMediatRServices();
            services.AddDbContextServices(configuration);
            services.AddSerilog(configuration);
            services.AddSingleton<JwtTokenHelper>();
            services.AddHttpContextAccessor();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.
            services.AddControllers()
                .AddNewtonsoftJson(); // Enable Newtonsoft.Json for controllers

            // Add API versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Add versioned API explorer
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Register the Swagger generator with versioned API documents
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport(); // Enable support for Newtonsoft.Json in Swagger
            
                services.ConfigureOptions<ConfigureSwaggerBearerAuthOptions>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        private static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Serilog for logging to Elasticsearch
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information)
                 .CreateLogger();


            return services;
        }



    }
}
