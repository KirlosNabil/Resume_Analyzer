using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.Service.DTOs;

namespace Resume_Analyzer.Service.IServices
{
    public interface IAIModelService
    {
        public Task<ResumeAIResultDTO> MatchResumeJob(string userId);
    }
}
