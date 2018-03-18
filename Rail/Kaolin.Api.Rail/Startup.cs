using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kaolin.Api.Rail
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("angular_app", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.Configure<Infrastructure.Database.Config>(config =>
            {
                config.ConnectionString = "mongodb://localhost:27017";
                config.Database = "kaolin";
                config.CountriesCollection = "countries";
                config.StationsCollection = "stations";
            })
                .AddSingleton<Infrastructure.Database.CountriesDbContext>()
                .AddSingleton<Infrastructure.Database.StationsDbContext>()
                .AddMongoDbSessionProvider()
                .Configure<Infrastructure.SessionStore.Config>(config =>
                {
                    config.ConnectionString = "mongodb://localhost:27017";
                    config.Database = "ssp";
                    config.Collection = "sessions";
                })
                .Configure<Services.PassRzdRu.Parser.Config>(config =>
                {
                    config.Polling = new Services.PassRzdRu.Parser.Config.PollingConfig(60, 1000);
                })
                .Configure<Services.PassRzdRu.RailClient.Config>(config =>
                {
                    config.Username = "your_login_here";
                    config.Password = "your_password_here";
                })
                .AddSingleton<Services.PassRzdRu.Parser.PassRzdRuClient>()
                .AddTransient<Kaolin.Models.Rail.Abstractions.IRailClient, Services.PassRzdRu.RailClient.PassRzdRuRailClient>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCors("angular_app");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
