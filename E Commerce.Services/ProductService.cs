using AutoMapper;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Intreface;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
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

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            // Specifications => Get All Products Including Product Type and Product Brand
            // And Filter With BrandId Or TypeId If Needed

            var spec = new ProductWithTypeAndBrandSpecifications(queryParams);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            // ProductDTO الي Product هحول من
            var DataToReturn = mapper.Map<IEnumerable<ProductDTO>>(products);  // اللي راجعه Product دي شايله كل ال
            var CountOfReturnedData = DataToReturn.Count();      // Product بتاع كل ال Count دي شايله مجموع او ال
            var CountSpec = new ProductCountSpecifications(queryParams);
            var CountOfAllProducts = await unitOfWork.GetRepository<Product, int>().CountAysnc(CountSpec);
            //                                            (  PageIndex   ,  PageSize  ,         Count ,                 Data )
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, CountOfReturnedData, CountOfAllProducts, DataToReturn);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypeAsyncc()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            // TypeDTO الي  ProductType هحول من
            return mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            // Specification -> Get Producr By [Id] Including [ ProductType And ProductBrand ]
            var spec = new ProductWithTypeAndBrandSpecifications(id);

            var product = await unitOfWork.GetRepository<Product , int>().GetByIdAsync(spec);
            // ProductDTO  الي Product هحول من
            return mapper.Map<ProductDTO>(product);
        }
    }
}
