using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioBook.Api.Services;
using AudioBook.Api.Services.Interfaces;
using AudioBook.API;
using AudioBook.API.Providers;
using AudioBook.Core.Constants;
using AudioBook.Infrastructure;
using AudioBook.Infrastructure.Repositories;
using AudioBook.Infrastructure.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace AudioBook.Api
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

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            //var builder = services.AddIdentityServer()
            //    //.AddSigningCredential(cert)
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //    .AddInMemoryApiResources(Config.GetApiResources())
            //    .AddProfileService<IdentityProfileService>()
            //    .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AppConnections>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<IConfiguration>(Configuration);

            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton<AppSettings>(appSettings);

            var appConnections = new AppConnections();
            Configuration.Bind("ConnectionStrings", appConnections);
            services.AddSingleton<AppConnections>(appConnections);

            var authenticationCfg = new AuthenticationCfg();
            Configuration.Bind("Authentication", authenticationCfg);
            services.AddSingleton<AuthenticationCfg>(authenticationCfg);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAudioBookRepository, AudioBookRepository>();

            //services.AddScoped<IFileProvider, PhysicalFileProvider>();
            //services.AddScoped<IUnitOfWork, DapperUnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthorService, AuthorService>();

            //Pipeline
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            var assembly = AppDomain.CurrentDomain.Load("AudioBook.Api");
            services.AddMediatR(assembly);

            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(option =>
                {
                    option.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(option => option.RegisterValidatorsFromAssembly(assembly));

            services
                .AddApiVersioning(options =>
                {
                    options.ApiVersionReader = new MediaTypeApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                    options.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

            services.AddMvc();

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            ////.AddJwtBearer(o =>
            ////{
            ////    o.Authority = "http://localhost:5000";
            ////    o.Audience = "audiobooks";
            ////    o.RequireHttpsMetadata = false;
            ////});
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = authenticationCfg.Authority;
            //    options.ApiName = authenticationCfg.Audience;
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //});

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
                                Title = $"AudioBook API {description.ApiVersion}",
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
            if (env.IsDevelopment() || /*Customize*/ env.IsTest())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
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

            //app.UseIdentityServer();
            //app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseWelcomePage(new WelcomePageOptions() { Path = "/" });
            app.UseMvc();
        }
    }
}
