using TaskFocus.WebApi.Core.Models.Settings;

namespace TaskFocus.WebApi.Core.Interfaces;

public interface ITaskManagerUserSettingsService
{
    public Task<TaskManagerUserSettingsResponseModel> GetTaskManagerUserSettings(Guid userId);

    public Task<TaskManagerUserSettingsResponseModel> UpdateTaskManagerUserSettings(Guid userId,
        UpdateTaskManagerUserSettingsModel updateTaskManagerUserSettingsModel);
}