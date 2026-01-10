using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LearnASPDotNet.Middlewares
{
    public static class JwtAuthentication
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!,
                        ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!)
                        ),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context => // Xử lý khi xác thực thất bại
                        {
                            context.NoResult(); // Ngăn phản hồi mặc định
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            string message = "Authentication failed.";
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                message = "Token has expired.";
                            }
                            else
                            {
                                message = "Invalid token.";
                            }
                            var result = System.Text.Json.JsonSerializer.Serialize(new { error = message });
                            Console.WriteLine("OnAuthenticationFailed: " + result);
                            context.HttpContext.Items["AuthError"] = message;
                            return Task.CompletedTask; // End the task if authentication fails it should not proceed further
                            //return context.Response.WriteAsync(result);

                        },
                        OnTokenValidated = context => // Xử lý khi token hợp lệ
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        },

                        OnChallenge = async context => // Xử lý khi không có token hoặc token không hợp lệ
                        {
                            // Ngăn phản hồi mặc định
                            context.HandleResponse();

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            string errorMessage = "Unauthorized access: Token is missing or invalid";

                            if (context.HttpContext.Items.ContainsKey("AuthError"))
                            {
                                errorMessage = context.HttpContext.Items["AuthError"] as string ?? errorMessage; // Lấy thông điệp lỗi từ Items nếu có
                            }

                            var result = System.Text.Json.JsonSerializer.Serialize(new { error = errorMessage });
                            await context.Response.WriteAsync(result);
                        },

                        OnForbidden = async context => // Xử lý khi người dùng không có quyền truy cập
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";

                            var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Forbidden: You do not have access" });
                            await context.Response.WriteAsync(result);
                        }

                    };

                });

            return services;
        }
    }
}
