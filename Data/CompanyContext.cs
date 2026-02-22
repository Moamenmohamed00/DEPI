using System;
using System.Collections.Generic;
using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
namespace CompanyEFCore.Data;

public partial class CompanyContext : DbContext
{
    public CompanyContext() : base()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }
    public DbSet<Course> courses { get; set; }
    public DbSet<EssayQuestion> essayQuestions { get; set; }
    public DbSet<Exam> exams { get; set; }
    public DbSet<ExamAttempt> examAttempts { get; set; }
    public DbSet<Instructor> instructors {  get; set; }
    public DbSet<InstructorCourse> instructorCourses { get; set; }
    public DbSet<MulitpleChoiseQuestion> mulitpleChoiseQuestions { get; set; }
    public DbSet<Question> questions { get; set; }
    public DbSet<Student> students { get; set; }
    public DbSet<StudentAnswer> StudentAnswers {  get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Company;Integrated Security=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyContext).Assembly);
        //    OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<StudentCourse>()
        //.HasKey(sc => new { sc.StudentId, sc.CourseId });

        //    modelBuilder.Entity<InstructorCourse>()
        //        .HasKey(ic => new { ic.InstructorId, ic.CourseId });
        //    modelBuilder.Entity<Student>()
        //.HasIndex(s => s.Email)
        //.IsUnique();

        //    modelBuilder.Entity<Student>()
        //        .HasIndex(s => s.StudentNumber)
        //        .IsUnique();

        //    modelBuilder.Entity<Instructor>()
        //        .HasIndex(i => i.Email)
        //        .IsUnique();
        //    modelBuilder.Entity<Course>()
        //.HasMany(c => c.Exams)
        //.WithOne(e => e.Course)
        //.HasForeignKey(e => e.CourseId)
        //.OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Exam>()
        //        .HasMany(e => e.Questions)
        //        .WithOne(q => q.Exam)
        //        .HasForeignKey(q => q.Id)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<ExamAttempt>()
        //.HasMany(a => a.StudentAnswers)
        //.WithOne(sa => sa.ExamAttempt)
        //.HasForeignKey(sa => sa.ExamAttemptId)
        //.OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<Student>()
        //.HasMany(s => s.ExamAttempts)
        //.WithOne(a => a.Student)
        //.HasForeignKey(a => a.StudentId)
        //.OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Exam>()
        //.HasCheckConstraint("CK_Exam_EndDate", "EndDate > StartDate");

        //    modelBuilder.Entity<Question>()
        //        .HasCheckConstraint("CK_Question_Marks", "Marks > 0");

        //    modelBuilder.Entity<Course>()
        //        .HasCheckConstraint("CK_Course_MaxDegree", "MaximumDegree > 0");
        //    modelBuilder.Entity<Student>()
        //.HasIndex(s => s.Email);

        //    modelBuilder.Entity<Exam>()
        //        .HasIndex(e => e.StartDate);

        //    modelBuilder.Entity<ExamAttempt>()
        //        .HasIndex(a => a.StartTime);
        //    modelBuilder.Entity<Question>()
    //.HasDiscriminator<QueationType>("QuestionType")
    //.HasValue<MulitpleChoiseQuestion>(QueationType.MultipleChoice)
    //.HasValue<TrueFalseQuestion>(QueationType.TrueFalse)
    //.HasValue<EssayQuestion>(QueationType.Essay);
    }

}
