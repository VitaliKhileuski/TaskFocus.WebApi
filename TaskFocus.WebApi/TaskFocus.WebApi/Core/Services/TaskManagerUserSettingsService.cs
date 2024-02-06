using AutoMapper;
using TaskFocus.Data.Interfaces;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Settings;

namespace TaskFocus.WebApi.Core.Services;

public class TaskManagerUserSettingsService : ITaskManagerUserSettingsService
{
    private readonly ITaskManagerUserSettingsRepository _taskManagerUserSettingsRepository;
    private readonly IMapper _mapper;

    public TaskManagerUserSettingsService(
        ITaskManagerUserSettingsRepository taskManagerUserSettingsRepository,
        IMapper mapper)
    {
        _taskManagerUserSettingsRepository = taskManagerUserSettingsRepository;
        _mapper = mapper;
    }

    public async Task<TaskManagerUserSettingsResponseModel> GetTaskManagerUserSettings(Guid userId)
    {
        var taskManagerUserSettingsEntity = await _taskManagerUserSettingsRepository.FindAsync(x => x.UserId == userId);

        var taskManagerUserSettingsResponseModel =
            _mapper.Map<TaskManagerUserSettingsResponseModel>(taskManagerUserSettingsEntity);

        return taskManagerUserSettingsResponseModel;
    }

    public async Task<TaskManagerUserSettingsResponseModel> UpdateTaskManagerUserSettings(Guid userId,
        UpdateTaskManagerUserSettingsModel updateTaskManagerUserSettingsModel)
    {
        var taskManagerUserSettingsEntity = await _taskManagerUserSettingsRepository.FindAsync(x => x.UserId == userId);

        _mapper.Map(updateTaskManagerUserSettingsModel, taskManagerUserSettingsEntity);

        var taskManagerUserSettingsUpdatedEntity = await 
            _taskManagerUserSettingsRepository.UpdateAsync(taskManagerUserSettingsEntity);
        
        var taskManagerUserSettingsResponseModel =
            _mapper.Map<TaskManagerUserSettingsResponseModel>(taskManagerUserSettingsUpdatedEntity);

        return taskManagerUserSettingsResponseModel;
    }
}