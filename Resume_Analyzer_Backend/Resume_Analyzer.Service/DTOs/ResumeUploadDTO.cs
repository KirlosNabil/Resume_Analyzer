using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Resume_Analyzer.Service.DTOs
{
    public class ResumeUploadDTO
    {
        public IFormFile ResumeFile { get; set; }
    }
}
