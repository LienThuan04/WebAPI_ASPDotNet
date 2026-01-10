using System.Text.Json;
using System.Net;

namespace LearnASPDotNet.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly Boolean isProduction = Convert.ToBoolean(Environment.GetEnvironmentVariable("PRODUCTION"));
        private readonly Boolean isDevelopment = Convert.ToBoolean(Environment.GetEnvironmentVariable("DEVELOPMENT"));
        public MiddlewareException(RequestDelegate next)
        {
            _next = next;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) // xử lý ngoại lệ và trả về phản hồi JSON
        {
            var statusCode = (int)HttpStatusCode.InternalServerError; // Explicitly cast HttpStatusCode to int
            var response = new
            {
                StatusCode = statusCode,
                Message = exception.Message, //bắn lỗi ra message với đầu vào là exception 
                StackTrace = this.isProduction == false &&
                    this.isDevelopment == true ? exception.StackTrace : null // Thông tin ngăn xếp lỗi (chỉ nên hiển thị trong môi trường phát triển)
            };
            var payload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode; // Use the explicitly cast statusCode
            return context.Response.WriteAsync(payload);
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
        
    }
}
