using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyEFCore.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(200)]
        public string Title {  get; set; }
        [StringLength(1000,ErrorMessage ="Can't be more than 1000 word")]
        public string? Description {  get; set; }
        [Required]
        [Column(TypeName ="decimal(3,2)")]
        public decimal MaximumDegree {  get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt {  get; set; }
        public bool IsActive {  get; set; }=true;
        //Navigation prop to mapp relations
        //hashset generic collection has only uniqe, unordered elments
        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        public ICollection<InstructorCourse> InstructorCourses { get; set; } = new HashSet<InstructorCourse>();
    }
}
