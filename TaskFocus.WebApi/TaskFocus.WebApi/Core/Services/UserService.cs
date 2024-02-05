using System.Security.Cryptography;
using AutoMapper;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.User;

namespace TaskFocus.WebApi.Core.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly ICryptographyService _cryptographyService;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository,
        ICryptographyService cryptographyService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _cryptographyService = cryptographyService;
    }

    public async Task<UserResponseModel> CreateUser(CreateUserModel createUserModel)
    {
        var isUserExistedByEmail = await _userRepository.IsUserExistedByEmail(createUserModel.Email);

        if (isUserExistedByEmail)
        {
            return null;
        }

        var salt = Guid.NewGuid().ToString();

        var hashedPassword = _cryptographyService.GenerateHash(createUserModel.Password, salt, new SHA256Managed());

        var userEntity = new UserEntity
        {
            Email = createUserModel.Email.ToLower(),
            HashedPassword = hashedPassword,
            Salt = salt,
            Name = createUserModel.Name,
            TaskManagerUserSettings = new TaskManagerUserSettingsEntity
            {
                StrictModelEnabled = false
            }
        };

        var createdUserEntity = await _userRepository.CreateAsync(userEntity);

        var userResponseModel = _mapper.Map<UserResponseModel>(createdUserEntity);

        return userResponseModel;
    }

    public async Task<UserResponseModel> GetUserByCredentials(UserCredentialsModel userCredentialsModel)
    {
        var userEntity = await _userRepository.FindAsync(x => x.Email == userCredentialsModel.Email);

        if (userEntity == null)
        {
            return null;
        }

        var hashedPassword = _cryptographyService.GenerateHash(userCredentialsModel.Password, userEntity.Salt, new SHA256Managed());

        if (userEntity.HashedPassword != hashedPassword)
        {
            return null;
        }

        var userModel = _mapper.Map<UserResponseModel>(userEntity);

        return userModel;
    }
}