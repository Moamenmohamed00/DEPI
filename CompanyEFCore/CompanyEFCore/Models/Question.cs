using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public abstract class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public decimal Marks { get; set; }
        public QuestionType QuestionType { get; set; }
        public DateTime CreatedAt { get; set; }

        public Exam Exam { get; set; }
        public int Examid { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new HashSet<StudentAnswer>();


    }
    public enum QuestionType
    {
        MultipleChoice = 1,
        TrueFalse = 2,
        Essay = 3
    }
}
