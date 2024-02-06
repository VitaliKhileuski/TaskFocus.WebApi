using Microsoft.AspNetCore.Mvc;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Tasks;

namespace TaskFocus.WebApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{

    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskModel createTaskModel)
    {
        var createdTaskModel = await _taskService.CreateTask(createTaskModel);
        
        return Ok(createdTaskModel);
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> SearchTasks(TasksSearchModel tasksSearchModel)
    {
        var tasks = await _taskService.SearchTasks(tasksSearchModel);
        
        return Ok(tasks);
    }

    [HttpPut]
    [Route("changeTaskState/taskId/{taskId:guid}")]
    public async Task<IActionResult> ChangeTaskState(Guid taskId)
    {
        await _taskService.ChangeTaskState(taskId);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(UpdateTaskModel updateTaskModel)
    {
        var updatedTaskModel = await _taskService.UpdateTask(updateTaskModel);
        
        return Ok(updatedTaskModel);
    }

    [HttpDelete]
    [Route("taskId/{taskId:guid}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        await _taskService.DeleteTask(taskId);
        
        return Ok();
    }
}