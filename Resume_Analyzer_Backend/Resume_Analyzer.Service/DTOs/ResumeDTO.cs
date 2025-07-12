using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resume_Analyzer.DataAccess.Models;

namespace Resume_Analyzer.Service.DTOs
{
    public class ResumeDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Content { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
