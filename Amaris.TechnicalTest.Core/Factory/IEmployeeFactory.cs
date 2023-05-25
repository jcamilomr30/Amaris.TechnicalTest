using Amaris.TechnicalTest.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amaris.TechnicalTest.Core.Factory
{
    public interface IEmployeeFactory
    {
        IProviderEmployee GetProviderType(string contractType = "AnualSalaryEmployee");
    }
}
