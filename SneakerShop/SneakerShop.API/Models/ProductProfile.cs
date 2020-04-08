using AutoMapper;
using SneakerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.Models
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(p => p.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(p => p.UnitPrice))
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(p => p.Supplier.CompanyName));
        }
    }
}
