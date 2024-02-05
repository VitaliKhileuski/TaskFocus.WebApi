namespace TaskFocus.WebApi.Core.Models.Authentication;

public class ValidateTokenModel
{
    public bool IsValid { get; set; }

    public Guid UserId { get; set; }
    
    public string Email { get; set; }

    public ValidateTokenModel(bool isValid)
    {
        IsValid = isValid;
        UserId = Guid.Empty;
    }

    public ValidateTokenModel()
    {
    }
}