
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        //[Authorize(Roles = "Product.List")]
        public IActionResult GetAll()
        {
            var product = _productService.GetList();

            if (product.Success)
            {
                return Ok(product.Data);
            }
            return BadRequest(product.Message);
        }

        [HttpPost("add")]
        public IActionResult AddProduct(Product product)
        {
            var p = _productService.Add(product);
            return Ok(p);
        }

        [HttpPost("transaction")]
        public IActionResult TransactionTest(Product product)
        {
            var p = _productService.TransactionOperation(product);

            if (p.Success)
            {
                return Ok(p.Message);
            }
            return BadRequest(p.Message);
        }
    }
}
