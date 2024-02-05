namespace TaskFocus.WebApi.Core.Models.Authentication;

public class LoginUserRequestModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}