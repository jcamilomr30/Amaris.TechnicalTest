using System;
using System.Collections.Generic;
using System.Text;

namespace Amaris.TechnicalTest.Core.Dtos
{
    public class EmployeeDto : EntityBase<int>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Decimal Salary { get; set; }
        public Decimal AnualSalary { get; set; }
    }
}
