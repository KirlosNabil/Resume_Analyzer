using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resume_Analyzer.Service.DTOs;
using Resume_Analyzer.Service.IServices;

namespace Resume_Analyzer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            await _userService.Register(registerDto);
            return Ok("Registered Successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _userService.Login(loginDto);
            return Ok(token);
        }
    }
}
