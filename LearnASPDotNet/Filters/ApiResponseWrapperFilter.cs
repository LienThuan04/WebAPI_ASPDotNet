using LearnASPDotNet.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearnASPDotNet.Filters
{
    public class ApiResponseWrapperFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // No prior action is required.
        }
        public void OnActionExecuted(ActionExecutedContext context) // After the action is executed, check for errors and package the result into ApiResponse.
        {
            if (context.Exception == null && context.Result is ObjectResult objectResult) // If no errors occur and the result is an ObjectResult (returning data), wrap the result in ApiResponse.
            {
                if (objectResult.Value is ApiResponse<object>) // if the result is already an ApiResponse, do not wrap it again to avoid nesting ApiResponse within ApiResponse.
                {
                    return; // Exit the method without modifying the result, allowing the existing ApiResponse to be returned as is.
                }
                var message = context.HttpContext.Items["MessageResponse"] as string ?? "Success"; // If available, retrieve the success message from HttpContext.Items; otherwise, use "Success" as the default.
                var apiResponse = new ApiResponse<object>( // Create a new ApiResponse object with the appropriate status code, message, data, and error code (null in this case since it's a successful response).
                    statusCode: objectResult.StatusCode ?? 200, // Use the status code from the ObjectResult if available; otherwise, default to 200 (OK).
                    message: message, // Use the retrieved message or the default "Success" message.
                    data: objectResult.Value, // Set the data property to the value from the ObjectResult, which contains the actual data being returned by the action.
                    errorCode: null // if the response is successful, the error code is set to null since there are no errors to report.
                );
                context.Result = new ObjectResult(apiResponse) // Replace the original ObjectResult with a new ObjectResult that contains the ApiResponse, ensuring that the response sent to the client is consistently formatted as an ApiResponse.
                {
                    StatusCode = apiResponse.StatusCode
                };
            }
            // Nếu có lỗi, MiddlewareException sẽ xử lý và trả về phản hồi lỗi, nên không cần xử lý ở đây
        }

    }
}
