using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string GenerateToken(JwtPayloadDto Payload)
    {
        var claims = new[]
        {
           new Claim("userId", Payload.Id),
           new Claim("userName", Payload.Username),
           new Claim("userEmail", Payload.Email),
           new Claim("phone", Payload?.Phone ?? ""),
           new Claim("address", Payload?.Address ?? ""),
       };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpiryInMinutes"]!)
            ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(string userId)
    {
        var Payload = new[] {
            new Claim("userId", userId)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshKey"]!));
        var refreshToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: Payload,
            expires: DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["Jwt:RefreshExpiryInDays"]!)
                ),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
    }

    public void SetCookedToken(HttpContext httpContext, string key, string value)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // không cho phép truy cập cookie từ JavaScript
            Expires = DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["JWT:RefreshExpiryInDays"]!) // thời gian sống của cookie
                ),
            Secure = true, // chỉ gửi cookie qua kết nối HTTPS
            SameSite = SameSiteMode.Strict, // chỉ gửi cookie đó khi request đến từ cùng một site
            Path = "/" // áp dụng cookie cho toàn bộ ứng dụng
        };
        //var httpContext = new HttpContextAccessor().HttpContext; // Lấy HttpContext hiện tại
        httpContext?.Response.Cookies.Append(key, value, cookieOptions); // Thiết lập cookie
    }

    public ClaimsPrincipal? ValidateRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshKey"]!);
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
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken validatedToken
            );
            return principal;
        }
        catch
        {
            return null;
        }
    }
}
