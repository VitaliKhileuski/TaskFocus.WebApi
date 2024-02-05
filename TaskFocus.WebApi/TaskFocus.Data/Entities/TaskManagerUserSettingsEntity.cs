using System;
using System.Collections.Generic;

namespace TaskFocus.Data.Entities;

public class TaskManagerUserSettingsEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public List<UserPriorityLevelEntity> Priorities { get; set; }
    
    public List<UserTaskLabelEntity> Labels { get; set; }
    
    public bool StrictModelEnabled { get; set; }
}