using LearnASPDotNet.Features.Files.Services;
using LearnASPDotNet.Features.Files.Repositories;

namespace LearnASPDotNet.Features.Files
{
    public static class FileDependency
    {
        public static IServiceCollection AddFileFeature(this IServiceCollection services)
        {
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}