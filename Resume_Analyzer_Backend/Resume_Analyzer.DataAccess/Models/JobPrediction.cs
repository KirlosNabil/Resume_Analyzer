using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume_Analyzer.DataAccess.Models
{
    public class JobPrediction
    {
        public string Title { get; set; }
        public double Confidence { get; set; }
    }
}
