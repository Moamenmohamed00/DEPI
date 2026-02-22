using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Models
{
    public class InstructorCourse
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }

        public DateTime AssignedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Instructor Instructor { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
}
