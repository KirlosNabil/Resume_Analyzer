using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resume_Analyzer.Service.DTOs;
using Resume_Analyzer.Service.IServices;
using Resume_Analyzer.DataAccess.Models;

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
        [HttpGet("get-resume")]
        public async Task<IActionResult> GetResume()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ResumeDTO resume = await _resumeService.GetResume(userId);
            return Ok(resume);
        }
        [HttpPut("update-resume")]
        public async Task<IActionResult> UpdateResume([FromForm] ResumeUploadDTO resumeUploadDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _resumeService.UpdateResume(resumeUploadDto.ResumeFile, userId);
            return Ok("Resume updated successfully");
        }
    }
}
