using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskFocus.WebApi.Core.Interfaces;
using TaskFocus.WebApi.Core.Models.Authentication;

namespace TaskFocus.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IAuthenticationService authService,
            IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestModel registerUserRequestModel)
        {
            var tokenPairModel = await _authService.RegisterUser(registerUserRequestModel);

            var tokenPairResponseModel = _mapper.Map<TokenPairResponseModel>(tokenPairModel);
            
            return Ok(tokenPairResponseModel);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetTokenPair([FromBody] LoginUserRequestModel credentials)
        {
            var tokenPairModel = await _authService.LoginUser(credentials);
            
            if (tokenPairModel == null)
            {
                return Unauthorized();
            }

            return Ok(tokenPairModel);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestModel refreshTokenModel)
        {
            var tokenResponseModel = _authService.RefreshToken(refreshTokenModel.RefreshToken);

            if (tokenResponseModel == null)
            {
                return Unauthorized();
            }

            return Ok(tokenResponseModel);
        }
    }
}