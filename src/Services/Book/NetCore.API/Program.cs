﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace NetCore.API
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            ////var builder = new ConfigurationBuilder()
            ////                .SetBasePath(Directory.GetCurrentDirectory())
            ////                .AddJsonFile("appsettings.json");
            ////Configuration = builder.AddCommandLine(args).Build();
            ////var url = $"{Configuration["Domain:Url"]}:{Configuration["Domain:Port"]}";

            const string PORT = "5000";
            var listeningUrl = $"http://localhost:{PORT}";

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(x => x.AddConsole())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                    config.AddCommandLine(args);
                })
                .UseStartup<Startup>()
                //.UseUrls(url)
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.ReadFrom.Configuration(ctx.Configuration)
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.RollingFile(
                        new JsonFormatter(renderMessage: true),
                        @"logs\log-{Date}.log");
                })
                .Build();
        }
    }
}
