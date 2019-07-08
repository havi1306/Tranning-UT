using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using File.Api.Services;
using File.Api.Services.Interfaces;
using File.Infrastructure;
using File.Infrastructure.Repositories;
using File.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.HttpOverrides;
using File.Api.Providers;

namespace File.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MigrationProvider.Register(Configuration.GetSection("ConnectionStrings:DbContext").Value);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Cors service.
            services.AddCors(options => options
                .AddPolicy("CorsPolicy", p => p
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            );

            // XSRF
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.Configure<AppConnections>(Configuration.GetSection("ConnectionStrings"));
            var appConnections = new AppConnections();
            Configuration.Bind("ConnectionStrings", appConnections);
            services.AddSingleton<AppConnections>(appConnections);

            services.AddSingleton<IConfiguration>(Configuration);

            // Repository
            services.AddScoped<IFileRepository, FileRepository>();

            // Service
            services.AddScoped<IFileService, FileService>();

            services
                .AddMvcCore()
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services
                .AddApiVersioning(options =>
                {
                    options.ApiVersionReader = new MediaTypeApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                    options.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(
                            description.GroupName,
                            new Info()
                            {
                                Title = $"File API {description.ApiVersion}",
                                Version = description.ApiVersion.ToString()
                            });
                    }

                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                    {
                        Name = "Authorization",
                        In = "header"
                    });

                    //options.OperationFilter<AuthorizationHeaderOperationFilter>();
                    options.DocumentFilter<SwaggerSecurityRequirementsDocumentFilter>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    //options.OAuthClientId("audiobookapp.ro.client");
                    //options.OAuthClientSecret("audiobookapp.secret");
                });

            //app.UseHttpsRedirection();
            app.UseWelcomePage(new WelcomePageOptions() { Path = "/" });
            app.UseMvc();
        }
    }
}
