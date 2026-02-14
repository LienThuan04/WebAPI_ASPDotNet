using System.Text.Json;
using System.Net;

namespace LearnASPDotNet.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public MiddlewareException(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) //middleware xử lý ngoại lệ toàn cục
        {
            try
            {
                await _next(context); // gọi middleware tiếp theo trong pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // xử lý ngoại lệ
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) // xử lý ngoại lệ và trả về phản hồi JSON
        {
            var statusCode = (int)HttpStatusCode.InternalServerError; // Explicitly cast HttpStatusCode to int
            var response = new
            {
                StatusCode = statusCode,
                Message = _env.IsDevelopment() ? exception.Message : "Internal Server Error", //bắn lỗi ra message với đầu vào là exception
                ErrorCode = "INTERNAL_SERVER_ERROR", // Mã lỗi tùy chỉnh
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null // Thông tin ngăn xếp lỗi (chỉ nên hiển thị trong môi trường phát triển)
            };
            var options = new JsonSerializerOptions // cấu hình tùy chọn cho JsonSerializer
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Sử dụng camelCase cho tên thuộc tính trong JSON
                //WriteIndented = true // Định dạng JSON đẹp hơn (có thụt lề)
            };
            var payload = JsonSerializer.Serialize(response, options);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode; // Use the explicitly cast statusCode
            return context.Response.WriteAsync(payload);
        }
    }
}
