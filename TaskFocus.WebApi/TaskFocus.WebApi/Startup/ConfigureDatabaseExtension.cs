using Microsoft.EntityFrameworkCore;
using TaskFocus.Data;

namespace TaskFocus.WebApi.Startup
{
    public static class ConfigureDatabaseExtension
    {
        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(opt => { opt.UseNpgsql(configuration.GetConnectionString("db")); });
        }
    }
}