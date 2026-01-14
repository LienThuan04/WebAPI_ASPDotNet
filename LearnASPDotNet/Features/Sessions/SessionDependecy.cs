namespace LearnASPDotNet.Features.Sessions
{
    public static class SessionDependecy
    {
        public static IServiceCollection AddSessionFeature(this IServiceCollection services)
        {

            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionService, SessionService>();
            return services;
        }
    }
}
