using AutoMapper;
using Amaris.TechnicalTest.Core.Dtos;
using Amaris.TechnicalTest.Domain.Entities;
using Amaris.TechnicalTest.Core.Dtos.Response;

namespace Amaris.TechnicalTest.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeResponseDetail, EmployeeDto>();
        }
    }
}
