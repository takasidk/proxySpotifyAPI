using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SpotifyProxyAPI.Middlewares;
using SpotifyProxyAPI.Models;
using SpotifyProxyAPI.Repositories;
using SpotifyProxyAPI.Repositories.Interfaces;

namespace SpotifyProxyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(options =>
            {
                Configuration.GetSection("DatabaseSettings").Bind(options);
            });
            services.Configure<UserSettings>(options =>
            {
                Configuration.GetSection("UserSettings").Bind(options);
            });
            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddSingleton<IDataRepository, DataRepository>();
            services.AddHttpClient();
            if (Configuration["UserSettings:EnableMiniProfiler"] == "True")
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = Configuration["UserSettings:BasePath"] + "/miniprofiler";
                });
            }

            services.Configure<ApiBehaviorOptions>(a =>
            {
                a.SuppressModelStateInvalidFilter = true;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            if (Configuration["UserSettings:EnableMiniProfiler"] == "True")
            {
                app.UseMiniProfiler();
            }
            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware(typeof(AuditMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
