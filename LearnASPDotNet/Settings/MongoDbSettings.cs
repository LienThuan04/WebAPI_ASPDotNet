using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearnASPDotNet.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null!;
    }

    public static class MongoDbServiceExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services) //IServiceCollection là giao diện đại diện cho một tập hợp các dịch vụ có thể được tiêm phụ thuộc
        {
            // Configure MongoDB settings
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION")!;
                options.DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB")!;
            });

            // Register MongoDB client
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            // Register MongoDB database
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return client.GetDatabase(settings.DatabaseName);
            });

            return services;
        }
    }


}
