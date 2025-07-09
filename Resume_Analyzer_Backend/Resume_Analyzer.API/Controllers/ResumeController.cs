using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resume_Analyzer.Service.DTOs;
using Resume_Analyzer.Service.IServices;

namespace Resume_Analyzer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }
        [HttpPost("upload-resume")]
        public async Task<IActionResult> UploadResume([FromForm] ResumeUploadDTO resumeUploadDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _resumeService.UploadResume(resumeUploadDto.ResumeFile, userId);
            return Ok("Resume uploaded successfully");
        }
    }
}
