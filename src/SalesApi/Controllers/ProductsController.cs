using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Interfaces;
using SalesApi.Application.Requests;
using SalesApi.Application.Validators;
using SalesApi.Domain.Models;
using SalesApi.Responses;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductService productService) : BaseController
    {
        private readonly IProductService _productService = productService;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _productService.CreateProduct(request);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<ICollection<Product>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();

            return Ok(products, "Products retrieved successfully");
        }
    }
}
