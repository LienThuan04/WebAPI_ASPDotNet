namespace LearnASPDotNet.Settings
{
    public static class ConfigCors
    {
        public const string PolicyName = "CorsPolicy"; // Define a constant for the CORS policy name to be used when applying the policy to controllers or endpoints

        public static IServiceCollection AddAppCors(this IServiceCollection services) //extension method to add CORS configuration to the service collection, allowing for method chaining in the Program.cs file with type (IServiceCollection)
        {
            var raw = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ?? null; // Get allowed origins from environment variable

            var origins = string.IsNullOrWhiteSpace(raw)
                ? Array.Empty<string>() : raw.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(o => o.Trim())
                                                                                               .ToArray(); // Split the string into an array of origins
            services.AddCors(options => // Add CORS policy to allow specified origins, methods, and headers
            {
                options.AddPolicy(PolicyName, builder => // Define a CORS policy with the specified name
                {
                    builder.WithOrigins(origins) // WithOrigins(origins) to restrict to specific origins.
                           //.AllowAnyOrigin() // Allow any origin to access the API. You can replace this with .
                           .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                           .AllowAnyHeader() // Allow any HTTP header in the request. This is useful for allowing custom headers or authentication headers.
                           .AllowCredentials(); // Allow credentials (cookies, authorization headers, etc.) to be included in cross-origin requests. This is necessary if your API requires authentication and you want to allow cross-origin requests to include credentials.
                });
            });
            return services; // Return the modified IServiceCollection to allow for method chaining
        }
    }
}
