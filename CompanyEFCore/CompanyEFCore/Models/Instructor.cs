using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public string Specialization { get; set; }
        public DateTime HireDate {  get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<InstructorCourse> InstructorCourses { get; set; } = new HashSet<InstructorCourse>();
        public ICollection<Exam> Exams { get; set; }= new HashSet<Exam>();
    }
}
