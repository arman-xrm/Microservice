using JwtAuthByRole.Common.Utils;
using JwtAuthByRole.Data.Entity;
using JwtAuthByRole.Models.RequestModels;
using JwtAuthByRole.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthByRole.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly JwtUtils _jwtUtils;

        public AuthController(IUserService userService, JwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var createdUser = await _userService.Register(user, user.Password);
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var user = await _userService.Login(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtUtils.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }

}
