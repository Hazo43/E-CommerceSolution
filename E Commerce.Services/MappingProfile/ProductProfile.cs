using AutoMapper;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Brand 
            CreateMap<ProductBrand, BrandDTO>();
           
            // Types
            CreateMap<ProductType, TypeDTO>();

            // Product ( {Get All} , {Get By Id} )
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.productType.Name))
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                // ياخد من عندها الصوره او الليمك بتاع الصوره ProductPictureUrlResolver هيعدي علي ال ProductDTO الي Product من Map كل ما ييجي يعمل
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());
              
      
               
                
        }
    }
}
