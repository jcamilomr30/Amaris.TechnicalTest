using Amaris.TechnicalTest.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amaris.TechnicalTest.Core.Providers
{
    public class AnualSalaryEmployeeProvider : IProviderEmployee
    {
        private const int Months = 12;

        public EmployeeDto Employee { get ; set ; }

        public  decimal GetSalary()
        {
            return this.Employee.Salary * Months;
        }
    }
}
