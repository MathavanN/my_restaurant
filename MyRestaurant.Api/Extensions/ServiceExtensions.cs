using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRestaurant.Api.Middleware;
using MyRestaurant.Api.Swagger;
using MyRestaurant.Api.Validators.V1;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Core;
using MyRestaurant.Models;
using MyRestaurant.Services;
using MyRestaurant.Services.Account;
using MyRestaurant.Services.Contracts;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace MyRestaurant.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });
        }
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<SuperAdminAccount>(configuration.GetSection("SuperAdminAccount"));
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; //only for DEV ENV
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:AccessTokenSecret"]))
                };
            });
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<MyRestaurantContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigurePasswordPolicy(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
        }
        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //To disable Auto Model Data Validation Response
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var validationErrors = actionContext.ModelState.Select(x => new { x.Key, x.Value.Errors.FirstOrDefault().ErrorMessage });
                        var errorCode = HttpStatusCode.BadRequest;
                        object error = new { ErrorCode = errorCode, ErrorType = errorCode.ToString(), ErrorMessage = validationErrors, ErrorDate = DateTime.Now };
                        return new BadRequestObjectResult(error);
                    };
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateServiceTypeDtoValidator>());
        }
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(GetServiceTypeDto));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddScoped<IRestaurantInfoRepository, RestaurantInfoRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<IRestaurantInfoService, RestaurantInfoService>();
            services.AddScoped<IAccountService, AccountService>();
        }
        public static void ConfigureMSSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyRestaurantContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RestaurantConnectionString"));
                options.UseLazyLoadingProxies(true);
                options.EnableSensitiveDataLogging(false);
            });

            services.AddScoped<IMyRestaurantContext, MyRestaurantContext>();
        }

        public static void ConfigureVersionedApiExplorer(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                //The format of the version added to the route URL
                options.GroupNameFormat = "'v'VVV";
                //Tells swagger to replace the version in the controller route
                options.SubstituteApiVersionInUrl = true;
            });
        }
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                //options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();
            });
        }
    }
}
