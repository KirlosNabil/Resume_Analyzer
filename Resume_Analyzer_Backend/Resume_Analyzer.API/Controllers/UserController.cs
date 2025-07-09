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
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            await _userService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _userService.Login(dto);
            return Ok(token);
        }
    }
}
