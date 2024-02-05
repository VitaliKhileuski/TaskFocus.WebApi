using TaskFocus.WebApi.Core.Models.User;

namespace TaskFocus.WebApi.Core.Interfaces;

public interface IUserService
{
    Task<UserResponseModel> CreateUser(CreateUserModel createUserModel);

    Task<UserResponseModel> GetUserByCredentials(UserCredentialsModel userCredentialsModel);
}