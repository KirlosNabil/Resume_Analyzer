using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Analyzer.DataAccess.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
