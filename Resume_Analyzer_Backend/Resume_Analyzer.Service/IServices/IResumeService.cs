using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Resume_Analyzer.DataAccess.Models;

namespace Resume_Analyzer.Service.IServices
{
    public interface IResumeService
    {
        public Task UploadResume(IFormFile resumeFile, string userId);
        public Task<Resume> GetResume(string userId);
    }
}
