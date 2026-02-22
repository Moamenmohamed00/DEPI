using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class MulitpleChoiseQuestion:Question
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC {  get; set; }
        public string OptionD { get; set; }
        public char CorrectAnswer {  get; set; }
    }
}
