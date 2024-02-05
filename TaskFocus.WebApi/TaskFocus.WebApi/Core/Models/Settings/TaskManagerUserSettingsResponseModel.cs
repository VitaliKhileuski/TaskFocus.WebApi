namespace TaskFocus.WebApi.Core.Models.Settings;

public class TaskManagerUserSettingsResponseModel
{
    public Guid UserId { get; set; }
    
    public List<UserTaskLabelModel> Labels { get; set; }
    
    public List<UserPriorityLevelModel> Priorities { get; set; }
    
    public bool StrictModeEnabled { get; set; }
}