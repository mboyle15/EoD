using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EngineeringOnDisplay2017.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringOnDisplay2017
{
    public class Startup
    {

        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            _config = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(_config);

            services.AddDbContext<AppDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {

                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            })
            .AddEntityFrameworkStores<AppDbContext>();


            

            services.AddScoped<IEodRepository, EodRepository>();

            services.AddTransient<DbInitializer>();

            services.AddLogging();

            // Add framework services and sets the json to write out with camel case rather then pascal case
            services.AddMvc(config => {

                //switch to Https for production
                if (_env.IsProduction())
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(config =>
            {
                config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer seeder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Index}/{id?}");
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}
