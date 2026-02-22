using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate {  get; set; }
        public decimal? Grade {  get; set; }
        public bool IsCompleted {  get; set; }=false;
    }
}
