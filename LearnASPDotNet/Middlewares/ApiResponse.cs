namespace LearnASPDotNet.Middlewares
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }


        public ApiResponse(int statusCode, string message, T? data = default, string? errorCode = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
        }

    }
}
