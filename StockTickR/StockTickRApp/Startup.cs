using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core.Enrichers;
using Serilog.Enrichers.AspNetCore.HttpContext;
using StockTickR.Clients;
using StockTickR.Hubs;
using StockTickRApp.Hubs;

namespace StockTickR {
    public class Startup {
        public Startup (IHostingEnvironment env) {
            Configuration = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath)
                .AddJsonFile ("appsettings.json", true, true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables ()
                .Build ();

            HostingEnvironment = env;
        }

        public static IConfigurationRoot Configuration { get; private set; }
        public IHostingEnvironment HostingEnvironment { get; }

        private Uri DatabaseServerUri => new Uri ("http://stockdatabase:8082");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            Log.Logger = new LoggerConfiguration ()
                .ReadFrom.Configuration (Configuration.GetSection ("Logging"))
                .Enrich.FromLogContext ()
                .Enrich.WithProperty ("Environment", HostingEnvironment.EnvironmentName)
                .WriteTo.Console (outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {EventId} {Message:lj} {Properties}{NewLine}{Exception}{NewLine}")
                .CreateLogger ();

            services.AddLogging (loggingBuilder => loggingBuilder.AddSerilog (dispose: true));
            services.AddSingleton (Log.Logger);

            services.AddSingleton (Configuration);
            services.AddSingleton (new StockClient (DatabaseServerUri));
            services.AddSingleton (new StockHubConnection (DatabaseServerUri));

            // Add framework services.
            services.AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_1)
                .AddRazorPagesOptions (o => {
                    o.Conventions.ConfigureFilter (new IgnoreAntiforgeryTokenAttribute ());
                });

            services.AddSignalR ()
                .AddMessagePackProtocol ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerfactory,
            IApplicationLifetime appLifetime) {
            loggerfactory.AddConsole (Configuration.GetSection ("Logging"))
                .AddDebug ()
                .AddSerilog ();

            app.UseSerilogLogContext (options => {
                options.EnrichersForContextFactory = context => new [] {
                new PropertyEnricher ("TraceIdentifier", context.TraceIdentifier),
                new PropertyEnricher ("Connection.LocalIpAddress", context.Connection.LocalIpAddress),
                new PropertyEnricher ("Connection.RemoteIpAddress", context.Connection.RemoteIpAddress)
                };
            });

            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register (Log.CloseAndFlush);

            app.UseStatusCodePagesWithReExecute ("/Home/Error", "?statusCode={0}");

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseFileServer ();

            app.UseSignalR (routes => {
                routes.MapHub<StockTickerHub> ("/stocks");
            });
        }
    }
}