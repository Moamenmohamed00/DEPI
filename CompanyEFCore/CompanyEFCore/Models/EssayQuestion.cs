using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class EssayQuestion:Question
    {
        public int? MaxWordCount { get; set; }
        public string GradingCriteria { get; set; }
    }
}
