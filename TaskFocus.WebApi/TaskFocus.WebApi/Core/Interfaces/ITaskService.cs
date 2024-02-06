using TaskFocus.Data.Repositories;
using TaskFocus.WebApi.Core.Models.Tasks;

namespace TaskFocus.WebApi.Core.Interfaces;

public interface ITaskService
{
    Task ChangeTaskState(Guid taskId);

    Task DeleteTask(Guid taskId);

    Task<TaskResponseModel> CreateTask(CreateTaskModel createTaskModel);

    Task<TaskResponseModel> UpdateTask(UpdateTaskModel updateTaskModel);

    Task<List<TaskResponseModel>> SearchTasks(TasksSearchModel tasksSearchModel);
}