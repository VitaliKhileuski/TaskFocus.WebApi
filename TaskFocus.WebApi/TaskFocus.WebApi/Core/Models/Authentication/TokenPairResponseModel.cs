namespace TaskFocus.WebApi.Core.Models.Authentication;

public class TokenPairResponseModel
{
    public string JwtToken { get; set; }

    public string RefreshToken { get; set; }
}