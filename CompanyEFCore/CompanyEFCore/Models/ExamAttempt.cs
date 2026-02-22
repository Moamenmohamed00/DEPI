using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class ExamAttempt
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? TotalScore { get; set; }
        public bool IsSubmitted { get; set; } = false;
        public bool IsGraded { get; set; } = false;

        public int StudentId { get; set; }
        public int ExamId { get; set; }

        public Student Student { get; set; } = null!;
        public Exam Exam { get; set; } = null!;
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }
}
