namespace TaskFocus.Data.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    
    public string HashedPassword { get; set; }
    
    public string Salt { get; set; }
    
    public string Name { get; set; }
    
    public TaskManagerUserSettingsEntity TaskManagerUserSettings { get; set; }
}