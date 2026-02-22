using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal TotalMarks {  get; set; }
        public TimeSpan Duration {  get; set; }
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive {  get; set; }=true;

        public Course Course { get; set; }
        public int CourseId {  get; set; }
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set; }


        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new HashSet<ExamAttempt>();
    }
}
