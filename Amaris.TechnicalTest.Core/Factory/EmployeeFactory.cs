using Amaris.TechnicalTest.Core.Factory;
using Amaris.TechnicalTest.Core.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amaris.TechnicalTest.Core
{
    public class EmployeeFactory : IEmployeeFactory
    {

        private readonly IServiceProvider serviceProvider;

        public EmployeeFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IProviderEmployee GetProviderType(string contractType = "AnualSalaryEmployee")
        {
            var providerType = Type.GetType($"Amaris.TechnicalTest.Core.Providers.{contractType}Provider");
            return ActivatorUtilities.CreateInstance(serviceProvider, providerType) as IProviderEmployee;
        }
    }
}
