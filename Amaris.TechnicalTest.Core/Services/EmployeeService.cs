using AutoMapper;
using Amaris.TechnicalTest.Core.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amaris.TechnicalTest.Core.Dtos.Response;
using Amaris.TechnicalTest.Core.Dtos;
using Amaris.TechnicalTest.Infraestructure.HttpClients;
using Amaris.TechnicalTest.Core.Providers;
using Amaris.TechnicalTest.Core.Factory;

namespace Amaris.TechnicalTest.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeManagerClient employeeManagerClient;
        private readonly IEmployeeFactory employeeFactory;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeManagerClient employeeManagerClient
            , IEmployeeFactory employeeFactory
            , IMapper mapper)
        {
            this.employeeManagerClient = employeeManagerClient;
            this.employeeFactory = employeeFactory;
            this.mapper = mapper;
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var response = await employeeManagerClient.GetAsync<EmployeeResponse>($"employee/{id}");
            var employee = mapper.Map<EmployeeDto>(response.employeeResponseDetail);
            if (employee != default)
                GetSalary(employee);
            return employee;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var response = await employeeManagerClient.GetAsync<EmployeeListResponse>($"employees");
            var employees = mapper.Map<IList<EmployeeDto>>(response.employeeResponseDetails);
            foreach (var item in employees)
            {
                GetSalary(item);
            }
            return employees.OrderBy(e => e.Name);
        }

        private void GetSalary(EmployeeDto item)
        {
            IProviderEmployee provider = employeeFactory.GetProviderType();
            provider.Employee = item;
            item.AnualSalary = provider.GetSalary();
        }
    }
}
