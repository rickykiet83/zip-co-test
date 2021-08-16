using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AutoMapper;

namespace AspNetCorePostgreSQLDockerApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerFullName,
                    src => src.MapFrom(x => string.Join(' ', x.Customer.FirstName, x.Customer.LastName)));
        }
    }
}