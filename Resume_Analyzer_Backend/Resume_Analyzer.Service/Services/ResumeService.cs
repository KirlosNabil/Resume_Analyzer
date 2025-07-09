using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Resume_Analyzer.Service.IServices;
using ServiceLayer.Exceptions;

namespace Resume_Analyzer.Service.Services
{
    public class ResumeService : IResumeService
    {
        public async Task UploadResume(IFormFile resumeFile, string userId)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                throw new BadRequestException("Invalid file");
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
        }
    }
}
