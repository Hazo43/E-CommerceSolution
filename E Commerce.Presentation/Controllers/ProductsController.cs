using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController( IProductService _productService)
        {
            productService = _productService;
        }

        // GetAllProducts

        [HttpGet]
        // GET : BaseUrl/api/Products [brandId? or typeId?] 
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        // GetProductById

        [HttpGet("{id}")]
        // GET : BaseUrl/api/Products/id
        public async Task<ActionResult<ProductDTO>> GetBroductById(int id)
        {
            var products = await productService.GetProductByIdAsync(id);
            return Ok(products);
        }

        // GetAllProductTypes

        [HttpGet("types")]
        // GET : BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await productService.GetAllTypeAsyncc();
            return Ok(Types);
        }


        // GetAllProductBrands

        [HttpGet("brands")]
        // GET : BaseUrl/api/Products/prands
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await productService.GetAllBrandAsync();
            return Ok(Brands);
        }
    }
}
