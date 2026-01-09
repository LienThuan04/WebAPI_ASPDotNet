using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{

    public JwtService() //contructor
    {

    }

    public string GenerateToken(JwtPayloadDto Payload) // tạo access token với đầy đủ thông tin người dùng
    {
        if (Payload.Id == null || Payload.Username ==null || Payload.Email == null)
        {
            throw new ArgumentNullException(nameof(Payload));
        }
        var claims = new[]
        {
           new Claim("userId", Payload.Id),
           new Claim("userName", Payload.Username),
           new Claim("userEmail", Payload.Email),
           new Claim("phone", Payload?.Phone ?? ""),
           new Claim("address", Payload?.Address ?? ""),
       };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER")!,
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRE_MIN")!)
            ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(string userId) // tạo refresh token chỉ với userId
    {
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentNullException(nameof(userId));
        }
        var Payload = new[] {
            new Claim("userId", userId)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_REFRESH_KEY")!));
        var refreshToken = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER")!,
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
            claims: Payload,
            expires: DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS")!)
                ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
    }

    public void SetCookedToken(HttpContext httpContext, string key, string value) // thiết lập cookie cho refresh token
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // không cho phép truy cập cookie từ JavaScript
            Expires = DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("JWT_REFRESH_EXPIRE_DAYS")!) // thời gian sống của cookie
                ),
            Secure = true, // chỉ gửi cookie qua kết nối HTTPS
            SameSite = SameSiteMode.Strict, // chỉ gửi cookie đó khi request đến từ cùng một site
            Path = "/" // áp dụng cookie cho toàn bộ ứng dụng
        };
        //var httpContext = new HttpContextAccessor().HttpContext; // Lấy HttpContext hiện tại
        httpContext?.Response.Cookies.Append(key, value, cookieOptions); // Thiết lập cookie
    }

    public ClaimsPrincipal? ValidateRefreshToken(string refreshToken) // xác thực refresh token
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_REFRESH_KEY")!);
        try
        {
            var principal = tokenHandler.ValidateToken(
                refreshToken,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!,
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken
            );
            return principal;
        }
        catch ( Exception ex )
        {
            throw new SecurityTokenException("Invalid refresh token", ex);
        }
    }
}
