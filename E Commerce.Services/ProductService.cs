using AutoMapper;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Intreface;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService( IUnitOfWork _unitOfWork , IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync();
          
            // BrandDTO الي  ProductBrand  هحول من
            return mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            // ProductDTO الي Product هحول من
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypeAsyncc()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            // TypeDTO الي  ProductType هحول من
            return mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product , int>().GetByIdAsync(id);
           
            // ProductDTO  الي Product هحول من
            return mapper.Map<ProductDTO>(product);
        }
    }
}
