using AutoMapper;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Tasks;

namespace TaskFocus.WebApi.Core.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskService(
        ITaskRepository taskRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task ChangeTaskState(Guid taskId)
    {
        var taskEntity = await _taskRepository.FindAsync(x => x.Id == taskId);

        taskEntity.IsDone = !taskEntity.IsDone;

        await _taskRepository.UpdateAsync(taskEntity);
    }

    public async Task DeleteTask(Guid taskId)
    {
        await _taskRepository.DeleteByIdAsync(taskId);
    }

    public async Task<TaskResponseModel> CreateTask(CreateTaskModel createTaskModel)
    {
        var taskEntity = new TaskEntity
        {
            CreationDate = DateTimeOffset.Now,
            IsDone = false,
            LabelId = createTaskModel.LabelId,
            PriorityId = createTaskModel.PriorityId,
            Description = createTaskModel.Description
        };

        var createdTaskEntity = await _taskRepository.CreateAsync(taskEntity);

        var createdTaskModel = _mapper.Map<TaskResponseModel>(createdTaskEntity);

        return createdTaskModel;
    }

    public async Task<TaskResponseModel> UpdateTask(UpdateTaskModel updateTaskModel)
    {
        var taskEntity = await _taskRepository.FindByIdAsync(updateTaskModel.TaskId);

        taskEntity.LabelId = updateTaskModel.LabelId;
        taskEntity.PriorityId = updateTaskModel.PriorityId;
        taskEntity.Description = updateTaskModel.Description;

        var updatedTaskEntity = await _taskRepository.UpdateAsync(taskEntity);
        
        var updatedTaskModel = _mapper.Map<TaskResponseModel>(updatedTaskEntity);

        return updatedTaskModel;
        
    }

    public async Task<List<TaskResponseModel>> SearchTasks(TasksSearchModel tasksSearchModel)
    {
        var taskEntities = (await _taskRepository.GetAllAsync(x => x.CreationDate.Date == tasksSearchModel.Date.Date)).ToList();

        var taskModels = taskEntities.Select(_mapper.Map<TaskResponseModel>).ToList();

        return taskModels;
    }
}