using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var origins = ValidateConfig(builder.Configuration, "App:CorsOrigins");
const string _defaultCorsPolicyName = "localhost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(_defaultCorsPolicyName,
        policy =>
        {
            policy.WithOrigins(origins.Split(",", StringSplitOptions.RemoveEmptyEntries));
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.WithExposedHeaders("WWW-Authenticate");
            policy.AllowCredentials();
        });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.Configure<SuperAdminAccount>(builder.Configuration.GetSection("SuperAdminAccount"));

builder.Services.AddScoped<IMyRestaurantSeedData, MyRestaurantSeedData>();

builder.Services.AddDbContext<MyRestaurantContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantConnectionString"));
    options.UseLazyLoadingProxies(true);
    options.EnableSensitiveDataLogging(false);
});

builder.Services.AddScoped<IMyRestaurantContext, MyRestaurantContext>();

builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<MyRestaurantContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
});

builder.Services.AddAuthentication(options =>
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
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTSettings:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:AccessTokenSecret"])),

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };
    options.Events = new JwtBearerEvents()
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build())
    .AddPolicy(ApplicationClaimPolicy.SuperAdminOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.SuperAdmin)))
    .AddPolicy(ApplicationClaimPolicy.AdminOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Admin)))
    .AddPolicy(ApplicationClaimPolicy.ReportOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Report)))
    .AddPolicy(ApplicationClaimPolicy.NormalOnly, policy => policy.Requirements.Add(new MyRestaurantAccessRequirement(ApplicationClaimType.Normal)));

builder.Services.AddSingleton<IAuthorizationHandler, MyRestaurantAccessHandler>();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
}).AddApiExplorer(options =>
{
    //The format of the version added to the route URL
    options.GroupNameFormat = "'v'VVV";
    //Tells swagger to replace the version in the controller route
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddAutoMapper(typeof(GetServiceTypeDto));

builder.Services.AddScoped<IUserAccessorService, UserAccessorService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IRestaurantInfoService, RestaurantInfoService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
builder.Services.AddScoped<IStockTypeService, StockTypeService>();
builder.Services.AddScoped<IStockItemService, StockItemService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IPurchaseOrderItemService, PurchaseOrderItemService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IGoodsReceivedNoteService, GoodsReceivedNoteService>();
builder.Services.AddScoped<IGoodsReceivedNoteItemService, GoodsReceivedNoteItemService>();
builder.Services.AddScoped<IGoodsReceivedNoteFreeItemService, GoodsReceivedNoteFreeItemService>();
builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IRestaurantInfoRepository, RestaurantInfoRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IStockTypeRepository, StockTypeRepository>();
builder.Services.AddScoped<IStockItemRepository, StockItemRepository>();
builder.Services.AddScoped<IUnitOfMeasureRepository, UnitOfMeasureRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderItemRepository, PurchaseOrderItemRepository>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IGoodsReceivedNoteRepository, GoodsReceivedNoteRepository>();
builder.Services.AddScoped<IGoodsReceivedNoteItemRepository, GoodsReceivedNoteItemRepository>();
builder.Services.AddScoped<IGoodsReceivedNoteFreeItemRepository, GoodsReceivedNoteFreeItemRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //To disable Auto Model Data Validation Response
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var validationErrors = actionContext.ModelState.Where(x => x.Value!.ValidationState == ModelValidationState.Invalid)
                                                            .Select(x => new { x.Key, Error = x.Value!.Errors.FirstOrDefault()!.ErrorMessage });
                        var errorCode = HttpStatusCode.BadRequest;
                        object error = new { ErrorCode = errorCode, ErrorType = errorCode.ToString(), ErrorMessage = validationErrors, ErrorDate = DateTime.Now };
                        return new BadRequestObjectResult(error);
                    };
                });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceTypeDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(_defaultCorsPolicyName);
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var groupName in descriptions.Select(description => description.GroupName))
        {
            options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static string ValidateConfig(IConfiguration configuration, string configName)
{
    var configValue = configuration.GetSection(configName).Value;
    if (string.IsNullOrWhiteSpace(configValue))
        throw new ArgumentException($"Cannot find the {configName} details.");
    return configValue;
}

public partial class Program { }