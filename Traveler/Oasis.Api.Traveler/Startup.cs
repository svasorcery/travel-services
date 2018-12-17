using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oasis.Api.Traveler.DbContexts;
using Oasis.Api.Traveler.Abstractions;
using Oasis.Api.Traveler.Services;

namespace Oasis.Api.Traveler
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
            services.Configure<OasisDbContextOptions>(Configuration.GetSection("OasisDatabase"));
            //services.AddTransient<OasisDbContext>();
            //services.AddTransient<ICatalogueRepository<Models.Person>, PersonsRepository>();
            //services.AddTransient<ICatalogueRepository<Models.Account>, AccountsRepository>();

            services.AddTransient<InMemoryStorageContext>();
            services.AddTransient<ICatalogueRepository<Models.Person>, InMemoryPersonsRepository>();
            services.AddTransient<ICatalogueRepository<Models.Account>, InMemoryAccountsRepository>();

            services.AddMvcCore()
               .AddAuthorization()
               .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "oasis_api";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
