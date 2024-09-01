using BankingPortal.Application.Features.Commands.Accounts.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BankingPortal.Infrastructure.Extensions;
using Serilog;
using System.Text;
using BankingPortal.Application.Validations;
using FluentValidation.AspNetCore;
using FluentValidation;
using BankingPortal.Application.Mapping;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddMediatR([typeof(Program).Assembly, typeof(LoginHandler).Assembly]);
// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();
builder.Services.AddAutoMapper(typeof(MappingProfile)) ;

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = jwtSettings["Issuer"],
           ValidAudience = jwtSettings["Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(secretKey)
       };
   });


var app = builder.Build();



// Configure CAP middleware
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseGlobalExceptionHandling();
app.UseSwaggerWithVersioning();
app.UseHttpsRedirection();
app.UseRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddApplicationServices(configuration);
}
