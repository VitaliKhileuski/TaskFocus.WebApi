using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskFocus.WebApi.Core.Configs;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Authentication;
using TaskFocus.WebApi.Core.Models.User;

namespace TaskFocus.WebApi.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _userService;
    private IAuthenticationService _authenticationServiceImplementation;

    public AuthenticationService(
        IOptions<JwtSettings> jwtSettings,
        IUserService userService)
    {
        _userService = userService;
        _jwtSettings = jwtSettings.Value;
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }
    
    public async Task<TokenPairResponseModel> RegisterUser(RegisterUserRequestModel registerUserRequestModel)
    {
        var createUserRequestModel = new CreateUserModel
        {
            Email = registerUserRequestModel.Email,
            Password = registerUserRequestModel.Password,
            Name = registerUserRequestModel.Name
        };

        var createUserResponseModel = await _userService.CreateUser(createUserRequestModel);

        if (createUserResponseModel != null)
        {
            var jwtToken = BuildToken(createUserResponseModel.Id, createUserResponseModel.Email, _jwtSettings.Lifetime);

            var refreshToken = BuildToken(createUserResponseModel.Id, createUserResponseModel.Email, _jwtSettings.RefreshTokenLifetime);

            var tokenPairModel = new TokenPairResponseModel
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };

            return tokenPairModel;
        }

        return null;
    }

    public async Task<TokenPairResponseModel> LoginUser(LoginUserRequestModel loginUserRequestModel)
    {
        var credentialsRequestModel = new UserCredentialsModel
        {
            Email = loginUserRequestModel.Email,
            Password = loginUserRequestModel.Password
        };

        var userModel = await _userService.GetUserByCredentials(credentialsRequestModel);

        if (userModel == null)
        {
            return null;
        }

        return new TokenPairResponseModel
        {
            JwtToken = BuildToken(userModel.Id, userModel.Email, _jwtSettings.Lifetime),
            RefreshToken = BuildToken(userModel.Id, userModel.Email, _jwtSettings.RefreshTokenLifetime)
        };
    }

    public string RefreshToken(string refreshToken)
    {
        var tokenValidationResult = IsValidRefreshToken(refreshToken);

        if (tokenValidationResult.IsValid)
        {

            var jwtToken = BuildToken(tokenValidationResult.UserId, tokenValidationResult.Email, _jwtSettings.Lifetime);

            return jwtToken;
        }

        return null;
    }


    private string BuildToken(Guid id, string email, int lifetimeInMinutes)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", id.ToString()),
            new Claim("email", email)
        };
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(lifetimeInMinutes));
        var jwt = new JwtSecurityToken(claims: claims,
            signingCredentials: credentials,
            expires: expires);

        return _jwtTokenHandler.WriteToken(jwt);
    }
    
    private ValidateTokenModel IsValidRefreshToken(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            return new ValidateTokenModel(false);
        }

        var decodedToken = _jwtTokenHandler.ReadJwtToken(refreshToken);

        if (decodedToken.ValidTo.CompareTo(DateTime.UtcNow) < 0)
        {
            return new ValidateTokenModel(false);
        }

        try
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            _jwtTokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false
            }, out _);
        }
        catch
        {
            return new ValidateTokenModel(false);
        }

        var userId = decodedToken.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
        
        var email = decodedToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;

        var isUserIdGuid = Guid.TryParse(userId, out var guidUserId);

        if (isUserIdGuid)
        {
            return new ValidateTokenModel
            {
                IsValid = true,
                UserId = guidUserId,
                Email = email
            };
        }

        return new ValidateTokenModel(false);
    }
}