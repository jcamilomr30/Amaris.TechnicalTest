using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Amaris.TechnicalTest.Core.Dtos.Response
{
    public class EmployeeResponse : BaseResponse
    {
        [JsonProperty(PropertyName = "data")]
        public EmployeeResponseDetail employeeResponseDetail { get; set; }
    }
}
