using AspNetCorePostgreSQLDockerApp.Dtos;
using AspNetCorePostgreSQLDockerApp.Models;
using AutoMapper;

namespace AspNetCorePostgreSQLDockerApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>();
            
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderForUpdateDto, Order>();
            CreateMap<OrderForManipulationDto, Order>().ReverseMap();
        }
    }
}