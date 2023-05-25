using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Amaris.TechnicalTest.Core.Extensions;
using Serilog;
using Amaris.TechnicalTest.Infraestructure.HttpClients;

namespace Amaris.TechnicalTest.Web
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
            services.AddControllersWithViews(mvcOpts =>
            {
                mvcOpts.Filters.Add(typeof(CustomExceptionFilterAttribute));
            });

            services.AddSingleton<Serilog.ILogger>
                    (x => new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger());

            services.AddHttpClient<IEmployeeManagerClient, EmployeeManagerClient>().ConfigureHttpClient((provider, client) =>
            {
                client.BaseAddress = new Uri(Configuration["ExternalServices:EmployeeManager:Uri"]);
            });


            services.AddServices(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
