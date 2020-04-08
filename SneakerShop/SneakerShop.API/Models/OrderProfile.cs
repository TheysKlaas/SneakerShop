using AutoMapper;
using SneakerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.Models
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(p => p.OrderId))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(p => p.Timestamp))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(p => p.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(p => p.User.LastName))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(p => p.TotalPrice));
                //.ForMember(dest => dest.Products.Select(p => p.Supplier), opt => opt.MapFrom(p => p.Products.Select(p => p.Product.Supplier.CompanyName)));
                // implement NOSQL
                //.ForMember(dest => dest.Products, opt => opt.MapFrom(oi => oi.Products.Select(p => p.Product)));
        }
    }
}
