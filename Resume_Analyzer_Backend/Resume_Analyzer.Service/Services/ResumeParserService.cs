using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.Service.IServices;
using UglyToad.PdfPig;

namespace Resume_Analyzer.Service.Services
{
    public class ResumeParserService : IResumeParserService
    {
        public async Task<string> ParseResume(string resumePath)
        {
            StringBuilder text = new StringBuilder();
            await Task.Run(() =>
            {
                using var pdf = PdfDocument.Open(resumePath);
                foreach (var page in pdf.GetPages())
                {
                    text.AppendLine(page.Text);
                }
            });
            return text.ToString();
        }
    }
}
