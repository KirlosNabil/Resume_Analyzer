using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Analyzer.Service.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
