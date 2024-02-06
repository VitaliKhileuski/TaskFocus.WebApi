using TaskFocus.Data.Interfaces;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Services;

namespace TaskFocus.WebApi.Startup
{
    public static class ServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<ITaskManagerUserSettingsService, TaskManagerUserSettingsService>();
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}