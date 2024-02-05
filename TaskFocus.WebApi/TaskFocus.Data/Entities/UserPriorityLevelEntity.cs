using System;

namespace TaskFocus.Data.Entities;

public class UserPriorityLevelEntity : BaseEntity
{
    public Guid TaskManagerUserSettingsId { get; set; }
    
    public string Name { get; set; }
}