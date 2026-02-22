using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyEFCore.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string StudentNumber { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new HashSet<ExamAttempt>();
    }
}
