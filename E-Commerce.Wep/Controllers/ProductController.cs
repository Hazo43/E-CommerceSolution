using E_Commerce.Wep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Wep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // دا URL دا لازم اكتب ال controller عشان اقدر اوصل لل
        // Get : BaseUrl /api/Product/10(id)     
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return new Product() { Id = id, Name = "Test" };
        }


        // دا URL دا لازم اكتب ال controller عشان اقدر اوصل لل
        // Get : BaseUrl /api/Product 
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll( string name)
        {
            return new List<Product>();
        }

        [HttpPost]
        // Post : BaseUrl /api/Product

        public ActionResult<Product> AddProduct( Product item)
        {
            return item;
        }

        [HttpPut]
        // PUT : BaseUrl /api/Product

        public ActionResult<Product> UpdateProduct( Product item)
        {
            return item;
        }

        [HttpDelete]
        // delete : BaseUrl/api/Product

        public ActionResult<Product> DeleteProduct( Product item)
        {
            return item;
        }

    }
}
