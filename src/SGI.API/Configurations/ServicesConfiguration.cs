﻿namespace SGI.API.Configurations
{
    #region Using

    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Domain.Helpers;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Newtonsoft.Json.Serialization;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SGI.Domain.Repositories;
    using SGI.DataEFCoreSQL.Repositories;
    using SGI.Domain.Supervisor;

    #endregion

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureSection = configuration.GetSection("Infrastructure");
            services.Configure<InfrastructureAppSettings>(infrastructureSection);
            var infrastructure = infrastructureSection.Get<InfrastructureAppSettings>();

            switch (infrastructure.Type)
            {
                case "SQL":
                    services
                        .AddScoped<IUserRepository, UserRepository>()
                        .AddScoped<IRoleRepository, RoleRepository>();

                    break;
                case "MySQL":
                    //services
                    //    .AddScoped<IUserRepository, DataEFCoreMySQL.Repositories.UserRepository>();
   
                    break;

                default:
                    services
                        .AddScoped<IUserRepository, UserRepository>()
                        .AddScoped<IRoleRepository, RoleRepository>();

                    break;
            }

            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<ISupervisor, Supervisor>();

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
            })
             .AddXmlDataContractSerializerFormatters()
             .ConfigureApiBehaviorOptions(setupAction =>
             {
                 setupAction.InvalidModelStateResponseFactory = context =>
                 {
                    // create a problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices
                         .GetRequiredService<ProblemDetailsFactory>();
                     var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                             context.HttpContext,
                             context.ModelState);

                    // add additional info not added by default
                    problemDetails.Detail = "See the errors field for details.";
                     problemDetails.Instance = context.HttpContext.Request.Path;

                    // find out which status code to use
                    var actionExecutingContext =
                           context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if ((context.ModelState.ErrorCount > 0) &&
                         (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                     {
                         problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                         problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                         problemDetails.Title = "One or more validation errors occurred.";

                         return new UnprocessableEntityObjectResult(problemDetails)
                         {
                             ContentTypes = { "application/problem+json" }
                         };
                     }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                     problemDetails.Title = "One or more errors on input occurred.";
                     return new BadRequestObjectResult(problemDetails)
                     {
                         ContentTypes = { "application/problem+json" }
                     };
                 };
             });

            return services;
        }

        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = new ReferenceLoopHandling());

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<JwtAppSettings>(jwtSection);

            // configure jwt authentication
            var jwtAppSettings = jwtSection.Get<JwtAppSettings>();
            var key = Encoding.ASCII.GetBytes(jwtAppSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build());
            });
    }
}