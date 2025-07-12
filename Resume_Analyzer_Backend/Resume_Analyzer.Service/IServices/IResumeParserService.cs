using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Analyzer.Service.IServices
{
    public interface IResumeParserService
    {
        public Task<string> ParseResume(string resumePath);
    }
}
