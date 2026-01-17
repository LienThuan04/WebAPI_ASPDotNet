using Supabase;

namespace LearnASPDotNet.Extensions.Supabase
{
    public static class SupabaseServiceExtensions
    {
        public static IServiceCollection AddSupabase(this IServiceCollection services)
        {
            var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");

            if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
            {
                throw new Exception("SUPABASE_URL and SUPABASE_KEY must be set in . env file");
            }

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            services.AddSingleton(provider =>
                new Client(supabaseUrl, supabaseKey, options)
            );

            Console.WriteLine($"✅ Supabase connected: {supabaseUrl}");

            return services;
        }
    }
}