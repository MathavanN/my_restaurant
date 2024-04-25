using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyRestaurant.Api.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
            var bearerSecurityScheme = CreateBearerOpenApiSecurityScheme();
            options.AddSecurityDefinition(bearerSecurityScheme.Reference.Id, bearerSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { bearerSecurityScheme, new List<string>()}
                });
        }
        static OpenApiSecurityScheme CreateBearerOpenApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "MyRestaurant API",
                Version = description.ApiVersion.ToString(),
                Description = "MyRestaurant API Document.",
                Contact = new OpenApiContact() { Name = "Mathavan N", Email = "mathavan@gmail.com", Url = new Uri("https://github.com/MathavanN") },
                //License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated.";

            return info;
        }
    }
}
