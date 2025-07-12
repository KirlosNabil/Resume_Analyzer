using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Resume_Analyzer.DataAccess.Models;
using Resume_Analyzer.DataAccess.Repositories;
using Resume_Analyzer.Service.DTOs;
using Resume_Analyzer.Service.IServices;
using ServiceLayer.Exceptions;

namespace Resume_Analyzer.Service.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IResumeParserService _resumeParserService;
        private readonly IMapper _mapper;

        public ResumeService(IResumeRepository resumeRepository, IResumeParserService resumeParserService, IMapper mapper)
        {
            _resumeRepository = resumeRepository;
            _resumeParserService = resumeParserService;
            _mapper = mapper;
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
        public async Task<ResumeDTO> GetResume(string userId)
        {
            Resume resume = await _resumeRepository.GetUserResume(userId);
            ResumeDTO resumeDTO = _mapper.Map<ResumeDTO>(resume);
            if (resume == null)
            {
                throw new NotFoundException("Resume not found");
            }
            return resumeDTO;
        }
        public async Task UpdateResume(IFormFile resumeFile, string userId)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                throw new BadRequestException("Invalid file");
            }
            if (!(await _resumeRepository.CheckIfResumeUploaded(userId)))
            {
                throw new BadRequestException("No resume uploaded to update");
            }
            var fileExtension = Path.GetExtension(resumeFile.FileName);
            if (fileExtension.ToLower() != ".pdf")
            {
                throw new BadRequestException("Only PDF files are allowed.");
            }
            var resumesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");
            var fileName = $"{userId}{fileExtension}";
            var filePath = Path.Combine(resumesPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resumeFile.CopyToAsync(stream);
            }

            string content = await _resumeParserService.ParseResume(filePath);
            var resume = await _resumeRepository.GetUserResume(userId);
            resume.Content = content;
            resume.UploadTime = DateTime.Now;
            await _resumeRepository.UpdateResume(resume);
        }
        public async Task DeleteResume(string userId)
        {
            if (!(await _resumeRepository.CheckIfResumeUploaded(userId)))
            {
                throw new BadRequestException("No resume uploaded to delete");
            }
            string fileExtension = ".pdf";
            var resumesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");
            var fileName = $"{userId}{fileExtension}";
            var filePath = Path.Combine(resumesPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await _resumeRepository.DeleteUserResume(userId);
        }
    }
}
