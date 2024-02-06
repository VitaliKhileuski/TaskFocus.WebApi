using TaskFocus.WebApi.Core.Models.Settings;

namespace TaskFocus.WebApi.Core.Models.Tasks;

public class TaskResponseModel
{
    public Guid Id { get; set; }
    
    public DateTimeOffset CreationDate { get; set; }
    
    public string Description { get; set; }
    
    public UserPriorityLevelModel Priority { get; set; }
    
    public UserTaskLabelModel Label { get; set; }
    
    public bool IsDone { get; set; }
}