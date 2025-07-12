using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Resume_Analyzer.DataAccess.Models;
using Resume_Analyzer.DataAccess.Repositories;
using Resume_Analyzer.Service.IServices;
using ServiceLayer.Exceptions;

namespace Resume_Analyzer.Service.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IResumeParserService _resumeParserService;
        public ResumeService(IResumeRepository resumeRepository, IResumeParserService resumeParserService)
        {
            _resumeRepository = resumeRepository;
            _resumeParserService = resumeParserService;
        }
        private async Task<Resume> CreateResume(string userId, string content)
        {
            Resume resume = new Resume()
            {
                UserId = userId,
                Content = content,
                UploadTime = DateTime.Now
            };
            return resume;
        }
        public async Task UploadResume(IFormFile resumeFile, string userId)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                throw new BadRequestException("Invalid file");
            }
            if(await _resumeRepository.CheckIfResumeUploaded(userId))
            {
                throw new BadRequestException("User already uploaded resume");
            }
            var fileExtension = Path.GetExtension(resumeFile.FileName);
            if (fileExtension.ToLower() != ".pdf")
            {
                throw new BadRequestException("Only PDF files are allowed.");
            }

            var resumesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");
            var fileName = $"{userId}{fileExtension}";
            var filePath = Path.Combine(resumesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resumeFile.CopyToAsync(stream);
            }

            string content = await _resumeParserService.ParseResume(filePath);
            Resume resume = await CreateResume(userId, content);
            await _resumeRepository.AddResume(resume);
        }
        public async Task<Resume> GetResume(string userId)
        {
            Resume resume = await _resumeRepository.GetUserResume(userId);
            if(resume == null)
            {
                throw new NotFoundException("Resume not found");
            }
            return resume;
        }
    }
}
