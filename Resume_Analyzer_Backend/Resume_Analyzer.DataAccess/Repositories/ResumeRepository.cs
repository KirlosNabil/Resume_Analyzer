using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace Resume_Analyzer.DataAccess.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly AppDbContext _dbContext;
        public ResumeRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddResume(Resume resume)
        {
            await _dbContext.Resumes.AddAsync(resume);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Resume> GetResumeById(int resumeId)
        {
            return await _dbContext.Resumes.Include(u => u.User)
                .FirstOrDefaultAsync(r => r.Id == resumeId);
        }
        public async Task<Resume> GetUserResume(string userId)
        {
            return await _dbContext.Resumes.Include(u => u.User)
                .FirstOrDefaultAsync(r => r.UserId == userId);
        }
        public async Task UpdateResume(Resume resume)
        {
             _dbContext.Resumes.Update(resume);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteUserResume(string userId)
        {
            var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.UserId == userId);
            if (resume != null)
            {
                _dbContext.Resumes.Remove(resume);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckIfResumeUploaded(string userId)
        {
            return await _dbContext.Resumes.AnyAsync(u => u.UserId == userId);
        }
    }
}
