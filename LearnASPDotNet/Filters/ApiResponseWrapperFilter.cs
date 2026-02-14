using LearnASPDotNet.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearnASPDotNet.Filters
{
    public class ApiResponseWrapperFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Không cần làm gì trước khi hành động được thực thi
        }
        public void OnActionExecuted(ActionExecutedContext context) // Sau khi hành động được thực thi, kiểm tra nếu không có lỗi và gói kết quả vào ApiResponse
        {
            if (context.Exception == null && context.Result is ObjectResult objectResult) // Nếu không có lỗi xảy ra và kết quả là một ObjectResult (trả về dữ liệu), gói kết quả vào ApiResponse
            {
                //var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
                if (objectResult.Value is ApiResponse<object>) // Nếu kết quả đã là ApiResponse, không cần gói lại
                {
                    return;
                }
                var message = context.HttpContext.Items["MessageResponse"] as string ?? "Success"; // Lấy thông điệp thành công từ HttpContext.Items nếu có, nếu không thì sử dụng "Success" làm mặc định
                var apiResponse = new ApiResponse<object>(
                    statusCode: objectResult.StatusCode ?? 200,
                    message: message,
                    data: objectResult.Value,
                    errorCode: null
                );
                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = apiResponse.StatusCode
                };
            }
            // Nếu có lỗi, MiddlewareException sẽ xử lý và trả về phản hồi lỗi, nên không cần xử lý ở đây
        }

    }
}
