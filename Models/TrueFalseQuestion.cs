using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class TrueFalseQuestion:Question
    {
        public bool CorrectAnswer {  get; set; }
    }
}
