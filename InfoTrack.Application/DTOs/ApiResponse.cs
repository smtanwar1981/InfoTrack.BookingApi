namespace InfoTrack.Application.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public T Value { get; set; }

    public ApiResponse(bool success, string error, T value)
    {
        Success = success;
        Error = error;
        Value = value;
    }

    public static ApiResponse<T> Ok(T value) => new ApiResponse<T>(true, string.Empty, value);
    public static ApiResponse<T> Fail(string error) => new ApiResponse<T>(false, error, default);
}
