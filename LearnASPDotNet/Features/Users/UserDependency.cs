namespace LearnASPDotNet.Features.Users
{
    public static class UserDependency
    {
        public static IServiceCollection AddUserFeature(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

    }
}
