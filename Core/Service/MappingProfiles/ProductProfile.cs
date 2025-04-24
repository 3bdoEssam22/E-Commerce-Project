using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTo>()
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());
            CreateMap<ProductType, TypeDTo>();
            CreateMap<ProductBrand, BrandDTo>();
        }
    }
}
