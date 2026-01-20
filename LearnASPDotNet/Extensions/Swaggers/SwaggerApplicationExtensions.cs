namespace LearnASPDotNet.Extensions.Swaggers
{
    public static class SwaggerApplicationExtensions
    {
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger(); // Enable Swagger middleware

            app.UseSwaggerUI(options => // Configure Swagger UI
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnASPDotNet API V1"); // Document title
                options.ConfigObject.PersistAuthorization = true; // Persist authorization data
            });

            return app;
        }
    }
}
