namespace TaskFocus.WebApi.Core.Models.User;

public class CreateUserModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}