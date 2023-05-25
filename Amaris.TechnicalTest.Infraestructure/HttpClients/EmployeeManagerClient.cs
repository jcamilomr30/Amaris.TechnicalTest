using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Amaris.TechnicalTest.Infraestructure.HttpClients
{
    public class EmployeeManagerClient : ApiClient, IEmployeeManagerClient
    {
        public EmployeeManagerClient(HttpClient httpClient, IConfiguration configuration) : base(httpClient)
        {
            ServiceName = "Employee Manager Application";
        }

    }
}
