using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace TaskFocus.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggersConfig = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
                .WriteTo.Console(new CompactJsonFormatter())
                .Enrich.FromLogContext();

            Log.Logger = loggersConfig.CreateLogger();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup.Startup>();
                })
                .UseSerilog();
    }
}
