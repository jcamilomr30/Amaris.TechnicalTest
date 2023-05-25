using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Amaris.TechnicalTest.Core.Dtos;
using Amaris.TechnicalTest.Core.Interfaces;

namespace Amaris.TechnicalTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpPost]
        public ActionResult Employees_Read()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();

            int? id = null;

            if (!string.IsNullOrEmpty(Request.Form["columns[0][search][value]"].FirstOrDefault()))
                id = Convert.ToInt32(Request.Form["columns[0][search][value]"].FirstOrDefault());

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var data = new List<EmployeeDto>();
            if (id.HasValue)
            {
                var employee = employeeService.GetEmployee(id.Value).Result;
                if (employee != default)
                    data.Add(employee);
            }
            else
                data = employeeService.GetEmployees().Result.ToList();
            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                switch (sortColumn)
                {
                    case "name":
                        if (sortColumnDir == "asc")
                            data = data.OrderBy(s => s.Name).ToList();
                        else
                            data = data.OrderByDescending(s => s.Name).ToList();
                        break;

                    default:
                        break;
                }
            }
            recordsTotal = data.Count();
            var result = data.Skip(skip).Take(pageSize).ToList();

            return Ok(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
