using SalesApi.Application.Validation;

namespace SalesApi.Responses;

public class ApiResponse
{
    public string Status { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];

    public static ApiResponse CreateAsValidationError(string error, string detail) =>
        CreateError("ValidationError", error, detail);

    public static ApiResponse CreateAsValidationError(IEnumerable<ValidationErrorDetail> errors) =>
        CreateError("ValidationError", errors);

    public static ApiResponse CreateAsNotFound(string error, string detail) =>
        CreateError("ResourceNotFound", error, detail);

    public static ApiResponse CreateAsNotFound(string message = "Resource not found") =>
        CreateError("ResourceNotFound", message, string.Empty);

    public static ApiResponse CreateError(string type, string error, string detail) =>
        CreateError(type,
        [
            new()
            {
                Error = error,
                Detail = detail,
            }
        ]);

    public static ApiResponse CreateError(string type, IEnumerable<ValidationErrorDetail> errors) => new()
    {
        Status = "error",
        Type = type,
        Message = errors.FirstOrDefault()?.Error ?? "Validation Failed",
        Errors = errors,
    };
}
