using Microsoft.AspNetCore.Mvc;
using ShopBridge.Data;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController: Controller
    {
        private IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody]Product product)
        {
            _service.AddProduct(product);
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAllProducts()
        {
            var allProducts = _service.GetAllProducts();
            return Ok(allProducts);
        }

        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product newProduct)
        {
            _service.UpdateProduct(id, newProduct);
            return Ok(newProduct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _service.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _service.GetAllCategories();
            return Ok(categories);
        }
    }
}