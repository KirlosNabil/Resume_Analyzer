using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.DataAccess.Models;

namespace Resume_Analyzer.Service.DTOs
{
    public class ResumeAIResultDTO
    {
        public List<JobPrediction> Predicted_Jobs { get; set; }
        public List<string> Skills { get; set; }
    }
}
