using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore.API.Providers;
using NetCore.API.Services;
using NetCore.Core.Constants;
using NetCore.Infrastructure;
using NetCore.Infrastructure.Repositories;
using NetCore.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace NetCore.API
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

            var builder = services.AddIdentityServer()
                //.AddSigningCredential(cert)
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddProfileService<IdentityProfileService>()
                .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();

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

            //// Register the IOptions object
            //services.Configure<AppConnections>(Configuration.GetSection("ConnectionStrings"));
            //// Explicitly register the settings object by delegating to the IOptions object
            //services.AddSingleton(resolver =>
            //    resolver.GetRequiredService<IOptions<AppConnections>>().);
            services.AddSingleton<IApplePushService, ApplePushService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAudioBookRepository, AudioBookRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            services.AddScoped<IFileService, FileService>();
            //services.AddScoped<IFileProvider, PhysicalFileProvider>();
            //services.AddScoped<IUnitOfWork, DapperUnitOfWork>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IUserService, UserService>();

            services
                .AddMvcCore()
                .AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV")
                .AddAuthorization()
                .AddJsonOptions(option =>
                {
                    option.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                })
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //.AddJwtBearer(o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.Audience = "audiobooks";
            //    o.RequireHttpsMetadata = false;
            //});
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authenticationCfg.Authority;
                options.ApiName = authenticationCfg.Audience;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.ReportApiVersions = true;
            });

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
                                Title = $"Push notification API {description.ApiVersion}",
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

                    // JWT-token authentication by password
                    //options.AddSecurityDefinition("Bearer", new OAuth2Scheme
                    //{
                    //    Type = "oauth2",
                    //    Flow = "password",
                    //    TokenUrl = "https://caterwinidentity.azurewebsites.net/connect/token",
                    //    AuthorizationUrl = "https://caterwinidentity.azurewebsites.net",
                    //    // Optional scopes
                    //    Scopes = new Dictionary<string, string>
                    //    {
                    //        { "offline_access", "caterwin" },
                    //    }
                    //});
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
            var logger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment() || /*Customize*/ env.IsTest())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                logger.LogInformation("Development environment");
            }
            else
            {
                app.UseHsts();
                logger.LogInformation($"Environment: {env.EnvironmentName}");
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

                    options.OAuthClientId("audiobookapp.ro.client");
                    options.OAuthClientSecret("audiobookapp.secret");
                });

            app.UseIdentityServer();
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseWelcomePage(new WelcomePageOptions() { Path = "/" });
            app.UseMvc();
        }
    }
}
