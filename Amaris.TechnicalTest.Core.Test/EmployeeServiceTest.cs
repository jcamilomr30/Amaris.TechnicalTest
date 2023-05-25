using Amaris.TechnicalTest.Core.Dtos;
using Amaris.TechnicalTest.Core.Dtos.Response;
using Amaris.TechnicalTest.Core.Factory;
using Amaris.TechnicalTest.Core.Interfaces;
using Amaris.TechnicalTest.Core.Providers;
using Amaris.TechnicalTest.Core.Services;
using Amaris.TechnicalTest.Infraestructure.HttpClients;
using AutoMapper;
using Moq;
using Serilog;
using System.Threading.Tasks;
using Xunit;

namespace Amaris.TechnicalTest.Core.Test
{
    public class EmployeeServiceTest
    {
        private readonly IEmployeeManagerClient employeeManagerClient;
        private readonly IEmployeeFactory employeeFactory;
        private readonly IMapper mapper;

        public EmployeeServiceTest()
        {
            employeeManagerClient = Mock.Of<IEmployeeManagerClient>();
            employeeFactory = Mock.Of<IEmployeeFactory>();
            mapper = Mock.Of<IMapper>();

            this.Target = new EmployeeService(
                employeeManagerClient,
                employeeFactory,
                mapper
                );
        }


        private IEmployeeService Target { get; set; }

        public class Method_GetEmployeeAsync : EmployeeServiceTest
        {
            public Method_GetEmployeeAsync()
            {
                var id = 1;
                Mock.Get(employeeManagerClient)
                    .Setup(edr => edr.GetAsync<EmployeeResponse>(It.IsAny<string>()))
                    .ReturnsAsync(new EmployeeResponse
                    {
                        employeeResponseDetail = new EmployeeResponseDetail
                        {
                            Id = id,
                            Salary = 1000
                        }
                    });

                Mock.Get(mapper)
              .Setup(x => x.Map<EmployeeDto>(It.IsAny<EmployeeResponseDetail>()))
              .Returns(new EmployeeDto
              {
                  Id = id,
                  Salary = 1000
              });

                Mock.Get(employeeFactory)
                      .Setup(edr => edr.GetProviderType(It.IsAny<string>()))
                      .Returns(new AnualSalaryEmployeeProvider());
            }

            [Fact]
            public async Task GetEmployee_Is_Called_Once_With_Specific_Id()
            {
                //Arrange


                //Act
                await Target.GetEmployee(It.IsAny<int>());

                //Assert
                Mock.Get(employeeManagerClient)
                    .Verify(edr => edr.GetAsync<EmployeeResponse>(It.IsAny<string>()), Times.Once);
            }

            [Fact]
            public async Task AnnualSalary_WhenGetEmployeeById()
            {
                //Arrange


                //Act
                var result = await Target.GetEmployee(It.IsAny<int>());

                //Assert
                Assert.Equal(result.AnualSalary, result.Salary * 12);
            }

            [Fact]
            public async void GetEmployee_Method_Returns_EmployeeDto_Type()
            {
                // Act
                var result = await Target.GetEmployee(It.IsAny<int>());

                // Assert
                Assert.IsType<EmployeeDto>(result);
            }
        }
    }
}