using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRestaurant.Api.Middleware;
using MyRestaurant.Api.PolicyHandlers;
using MyRestaurant.Api.Swagger;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Business.Validators.V1;
using MyRestaurant.Core;
using MyRestaurant.Models;
using MyRestaurant.SeedData;
using MyRestaurant.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Extensions
{
    public static class ServiceExtensions
    {
        private static string ValidateConfig(IConfiguration configuration, string configName)
        {
            var configValue = configuration.GetSection(configName).Value;
            if (string.IsNullOrWhiteSpace(configValue))
                throw new Exception($"Cannot find the {configName} details.");
            return configValue;
        }
        public static void ConfigureCors(this IServiceCollection services, string corsPolicyName, IConfiguration configuration)
        {
            var origins = ValidateConfig(configuration, "App:CorsOrigins");
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins(origins.Split(",", StringSplitOptions.RemoveEmptyEntries));
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.WithExposedHeaders("WWW-Authenticate");
                        policy.AllowCredentials();
                    });
            });
        }
        public static void ConfigureAuthorizationHandler(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, MyRestaurantAccessHandler>();
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.AddPolicy(ApplicationClaimPolicy.SuperAdminOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.SuperAdmin)));
                options.AddPolicy(ApplicationClaimPolicy.AdminOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Admin)));
                options.AddPolicy(ApplicationClaimPolicy.ReportOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Report)));
                options.AddPolicy(ApplicationClaimPolicy.NormalOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Normal)));
            });
        }

        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<SuperAdminAccount>(configuration.GetSection("SuperAdminAccount"));
        }

        public static void ConfigureDatabaseInitializer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMyRestaurantSeedData, MyRestaurantSeedData>();
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
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTSettings:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTSettings:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:AccessTokenSecret"])),

                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
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
                        var validationErrors = actionContext.ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                                                            .Select(x => new { Key = x.Key, Error = x.Value.Errors.FirstOrDefault().ErrorMessage });
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
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IRestaurantInfoRepository, RestaurantInfoRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStockTypeRepository, StockTypeRepository>();
            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<IPurchaseOrderItemRepository, PurchaseOrderItemRepository>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddScoped<IGoodsReceivedNoteRepository, GoodsReceivedNoteRepository>();
            services.AddScoped<IGoodsReceivedNoteItemRepository, GoodsReceivedNoteItemRepository>();
            services.AddScoped<IGoodsReceivedNoteFreeItemRepository, GoodsReceivedNoteFreeItemRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessorService, UserAccessorService>();
            services.AddScoped<IServiceTypeService, ServiceTypeService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IRestaurantInfoService, RestaurantInfoService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddScoped<IStockTypeService, StockTypeService>();
            services.AddScoped<IStockItemService, StockItemService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPurchaseOrderItemService, PurchaseOrderItemService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IGoodsReceivedNoteService, GoodsReceivedNoteService>();
            services.AddScoped<IGoodsReceivedNoteItemService, GoodsReceivedNoteItemService>();
            services.AddScoped<IGoodsReceivedNoteFreeItemService, GoodsReceivedNoteFreeItemService>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();
            services.AddScoped<ITransactionService, TransactionService>();
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
