using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Amaris.TechnicalTest.Core.Dtos.Response
{
    public class EmployeeListResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "data")]
        public List<EmployeeResponseDetail> employeeResponseDetails { get; set; }
    }

    public class EmployeeResponseDetail
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "employee_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "employee_age")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "employee_salary")]
        public Decimal Salary { get; set; }
    }
}
