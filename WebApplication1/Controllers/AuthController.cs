
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto user)
        {
            var userToLogin = _authService.Login(user);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpPost("register")]
        public IActionResult Register(CreateUserDto user)
        {
            var userExist = _authService.UserExists(user.Email);

            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }

            var registerResult = _authService.Register(user,user.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
