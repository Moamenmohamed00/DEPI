using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public string? AnswerText { get; set; }
        public char? SelectedOption { get; set; }
        public bool? BooleanAnswer { get; set; }
        public decimal? MarksObtained { get; set; }
        public DateTime SubmittedAt { get; set; }

        public int ExamAttemptId { get; set; }
        public int QuestionId { get; set; }

        public ExamAttempt ExamAttempt { get; set; } = null!;
        public Question Question { get; set; } = null!;
        //null! to remove warnning of nullable reference
    }
}
