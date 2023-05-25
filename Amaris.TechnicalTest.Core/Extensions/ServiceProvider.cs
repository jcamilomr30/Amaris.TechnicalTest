using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amaris.TechnicalTest.Core.Interfaces;
using Amaris.TechnicalTest.Core.Services;
using System.Reflection;
using Amaris.TechnicalTest.Infraestructure.HttpClients;
using Amaris.TechnicalTest.Core.Factory;

namespace Amaris.TechnicalTest.Core.Extensions
{
    public static class ServiceProvider
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            //Repositories

            //Services
            services.AddScoped<IEmployeeFactory, EmployeeFactory>();
            services.AddScoped<IEmployeeService, EmployeeService>();
         
            services.AddAutoMapper(Assembly.Load("Amaris.TechnicalTest.Core"));
          
        }
    }
}
