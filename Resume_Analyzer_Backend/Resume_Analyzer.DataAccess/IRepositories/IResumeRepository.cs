using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.DataAccess.Models;

namespace Resume_Analyzer.DataAccess.Repositories
{
    public interface IResumeRepository
    {
        public Task AddResume(Resume resume);
        public Task UpdateResume(Resume resume);
        public Task<Resume> GetUserResume(string userId);
        public Task<Resume> GetResumeById(int resumeId);
    }
}
