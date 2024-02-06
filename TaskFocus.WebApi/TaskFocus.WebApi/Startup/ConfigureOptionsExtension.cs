using TaskFocus.WebApi.Core.Configs;

namespace TaskFocus.WebApi.Startup
{
    public static class ConfigureOptionsExtension
    {
        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings))); 
        }
    }
}