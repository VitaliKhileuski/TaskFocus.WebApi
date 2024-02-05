using System;

namespace TaskFocus.Data.Entities;

public class UserTaskLabelEntity : BaseEntity
{
    public Guid TaskManagerUserSettingsId { get; set; }
    
    public string Name { get; set; }
}