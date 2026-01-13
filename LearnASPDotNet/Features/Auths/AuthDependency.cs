namespace LearnASPDotNet.Features.Auths
{
    public static class AuthDependency
    {
        public static IServiceCollection AddAuthFeature(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped
            services.AddScoped<JwtService>();
            return services;
        } 
    }
}
