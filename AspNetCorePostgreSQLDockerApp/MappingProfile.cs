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
            
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderForManipulationDto, Order>().ReverseMap();
        }
    }
}