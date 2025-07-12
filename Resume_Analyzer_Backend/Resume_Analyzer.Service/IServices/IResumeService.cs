using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Resume_Analyzer.DataAccess.Models;
using Resume_Analyzer.Service.DTOs;

namespace Resume_Analyzer.Service.IServices
{
    public interface IResumeService
    {
        public Task UploadResume(IFormFile resumeFile, string userId);
        public Task<ResumeDTO> GetResume(string userId);
        public Task UpdateResume(IFormFile resumeFile, string userId);
        public Task DeleteResume(string userId);
    }
}
