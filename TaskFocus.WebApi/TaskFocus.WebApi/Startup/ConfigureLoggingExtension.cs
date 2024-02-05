using Serilog;

namespace TaskFocus.WebApi.Startup
{
    public static class ConfigureLoggingExtension
    {
        public static void RegisterRequestLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
        }
    }
}