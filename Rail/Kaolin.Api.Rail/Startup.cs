using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaolin.Api.Rail
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2)
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "rail_api";
                });

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

            services.Configure<Infrastructure.Database.Config>(Configuration.GetSection("Database"))
                .AddSingleton<Infrastructure.Database.CountriesDbContext>()
                .AddSingleton<Infrastructure.Database.StationsDbContext>()
                .AddMongoDbSessionProvider()
                .Configure<Infrastructure.SessionStore.Config>(Configuration.GetSection("SessionStore"))
                .Configure<Services.PassRzdRu.Parser.Config>(config =>
                {
                    config.Polling = new Services.PassRzdRu.Parser.Config.PollingConfig(60, 1000);
                })
                .Configure<Services.PassRzdRu.RailClient.Config>(Configuration.GetSection("PassRzdRuRailClient"))
                .AddSingleton<Services.PassRzdRu.Parser.PassRzdRuClient>()
                .AddTransient<Kaolin.Models.Rail.Abstractions.IRailClient, Services.PassRzdRu.RailClient.PassRzdRuRailClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCors("angular_app");

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
