
public static class HttpContextExtensions
{
    public static JwtPayloadDto GetCurrentUser(this HttpContext context)
    {
        var user = context.User;

        return new JwtPayloadDto
        {
            UserId = user.FindFirst("userId")?.Value ?? "",
            Username = user.FindFirst("userName")?.Value ?? "",
            Email = user.FindFirst("userEmail")?.Value ?? "",
            Phone = user.FindFirst("phone")?.Value ?? "",
            Address = user.FindFirst("address")?.Value ?? ""
        };
    }
}

