using Business.Abstract;
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
        [Authorize]
        public IActionResult GetAll()
        {
            var product = _productService.GetList();

            if (product.Success)
            {
                return Ok(product.Data);
            }
            return BadRequest(product.Message);
        }

    }
}
