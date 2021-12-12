using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MyRestaurant.Api.Extensions;

namespace MyRestaurant.Api
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(_defaultCorsPolicyName, Configuration);

            services.ConfigureAppSettings(Configuration);

            services.ConfigureDatabaseInitializer();

            services.ConfigureMSSQLContext(Configuration);

            services.ConfigureIdentity();

            services.ConfigurePasswordPolicy();

            services.ConfigureAuthentication(Configuration);

            services.ConfigureAuthorization();

            services.ConfigureAuthorizationHandler();

            services.ConfigureVersionedApiExplorer();

            services.ConfigureApiVersioning();

            services.ConfigureAutoMapper();

            services.ConfigureServices();

            services.ConfigureRepositories();

            services.ConfigureController();

            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            app.UseApiVersioning();
            app.ConfigureCustomExceptionMiddleware();
            app.UseCors(_defaultCorsPolicyName);

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var groupName in provider.ApiVersionDescriptions.Select(x =>x.GroupName))
                    {
                        c.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
