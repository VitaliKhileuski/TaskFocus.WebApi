using System;

namespace TaskFocus.Data.Entities;

public class TaskEntity : BaseEntity
{
    public DateTimeOffset CreationDate { get; set; }
    
    public string Description { get; set; }
    
    public Guid? PriorityId { get; set; }
    
    public UserPriorityLevelEntity Priority { get; set; }
    
    public Guid? LabelId { get; set; }
    
    public UserTaskLabelEntity Label { get; set; }
    
    public bool IsDone { get; set; }
}