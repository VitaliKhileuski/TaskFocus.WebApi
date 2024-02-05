namespace TaskFocus.WebApi.Core.Models.Settings;

public class UpdateTaskManagerUserSettingsModel
{
    public List<UserTaskLabelModel> Labels { get; set; }
    
    public List<UserPriorityLevelModel> Priorities { get; set; }
    
    public bool StrictModeEnabled { get; set; }
}