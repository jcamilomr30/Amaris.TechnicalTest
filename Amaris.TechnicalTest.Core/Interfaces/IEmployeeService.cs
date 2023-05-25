using Amaris.TechnicalTest.Core.Dtos;
using Amaris.TechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amaris.TechnicalTest.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployee(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployees();
    }
}
