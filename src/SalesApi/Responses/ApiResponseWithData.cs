namespace SalesApi.Responses;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
