using System.Collections.Generic;
using System.Linq;
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
            CreateMap<Order, OrderDetailDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<Customer, CustomerOrdersDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));
            
            CreateMap<IEnumerable<Order>, CustomerOrdersDto>()
                .ForMember(dest => dest.Customer, opt =>
                {
                    opt.MapFrom(src => src.FirstOrDefault().Customer);
                })
                .ForMember(dest => dest.Orders, opts => 
                    opts.MapFrom(src => src)
                )
                ;
            
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderForUpdateDto, Order>();
            CreateMap<OrderForManipulationDto, Order>().ReverseMap();
        }
    }
}