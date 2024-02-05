using TaskFocus.Data.Interfaces;
using TaskFocus.Data.Repositories;

namespace TaskFocus.WebApi.Startup
{
    public static class RepositoriesExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskManagerUserSettingsRepository, TaskManagerUserSettingsRepository>();
        }
    }
}