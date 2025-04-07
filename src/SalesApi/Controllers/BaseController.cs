using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Validation;
using SalesApi.Responses;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Ok<T>(T data, string? message = default) =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Status = "success", Message = message ?? string.Empty });

        protected IActionResult Ok(string message) =>
            base.Ok(new ApiResponseWithData<object> { Data = null, Status = "success", Message = message });

        protected IActionResult Created<T>(string routeName, object routeValues, T data, string? message = default) =>
            base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Status = "success", Message = message ?? string.Empty });

        protected IActionResult BadRequest(List<ValidationFailure> errors) =>
            base.BadRequest(ApiResponse.CreateAsValidationError(errors.Select(ValidationErrorDetail.ConvertFrom)));

        protected IActionResult NotFound(string message = "Resource not found") =>
            base.NotFound(ApiResponse.CreateAsNotFound(message));
    }
}
