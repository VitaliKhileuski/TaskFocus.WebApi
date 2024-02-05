using TaskFocus.WebApi.Core.Models.Authentication;

namespace TaskFocus.WebApi.Core.Interfaces;

public interface IAuthenticationService
{
    Task<TokenPairResponseModel> RegisterUser(RegisterUserRequestModel registerUserRequestModel);

    Task<TokenPairResponseModel> LoginUser(LoginUserRequestModel loginUserRequestModel);

    string RefreshToken(string refreshToken);
}