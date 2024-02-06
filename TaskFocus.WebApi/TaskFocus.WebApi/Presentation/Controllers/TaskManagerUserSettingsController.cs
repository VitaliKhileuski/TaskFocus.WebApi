using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Settings;

namespace TaskFocus.WebApi.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TaskManagerUserSettingsController : ControllerBase
{
    private readonly ITaskManagerUserSettingsService _taskManagerUserSettingsService;

    public TaskManagerUserSettingsController(
        ITaskManagerUserSettingsService taskManagerUserSettingsService)
    {
        _taskManagerUserSettingsService = taskManagerUserSettingsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskManagerUserSettings()
    {
        var claims = User.Claims;
        
        var idClaim =
            claims.FirstOrDefault(x =>
                x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase))
            ?.Value ?? string.Empty;

        var userId = Guid.Parse(idClaim);
        
        var taskManagerUserSettings = await _taskManagerUserSettingsService.GetTaskManagerUserSettings(userId);
        
        return Ok(taskManagerUserSettings);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskManagerUserSettings(
        UpdateTaskManagerUserSettingsModel updateTaskManagerUserSettingsModel)
    {
        var userId = GetUserId();

        var taskManagerUserSettingsResponseModel =
            await _taskManagerUserSettingsService.UpdateTaskManagerUserSettings(userId,
                updateTaskManagerUserSettingsModel);

        return Ok(taskManagerUserSettingsResponseModel);
    }


    private Guid GetUserId()
    {
        var claims = User.Claims;
        
        var idClaim =
            claims.FirstOrDefault(x =>
                    x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                ?.Value ?? string.Empty;

        var userId = Guid.Parse(idClaim);

        return userId;
    }
}