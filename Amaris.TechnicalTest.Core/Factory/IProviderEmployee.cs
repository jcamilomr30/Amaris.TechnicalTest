using Amaris.TechnicalTest.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amaris.TechnicalTest.Core.Providers
{
    public interface IProviderEmployee
    {
        EmployeeDto Employee { get; set; }

        decimal GetSalary();
    }
}
