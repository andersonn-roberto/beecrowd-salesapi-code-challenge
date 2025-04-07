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
    public class SalesController(ISaleService saleService) : BaseController
    {
        private readonly ISaleService _saleService = saleService;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<Sale>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = validator.Validate(request);

            if (request.SaleLimitReached)
            {
                return BadRequest(new { Type = "BadRequest", Error = "Invalid Sell", Detail = "You cannot buy more than 20 pieces of same item" });
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var sale = await _saleService.CreateSale(request);

            return Ok(sale, "Sale created successfully");
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<ICollection<Sale>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseWithData<ICollection<Sale>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _saleService.GetSalesAsync();

            return Ok(sales, "Sales retrieved successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult CancelSale(Guid id)
        {
            _saleService.CancelSale(id);
            return Ok("Sale cancelled successfully");
        }
    }
}
