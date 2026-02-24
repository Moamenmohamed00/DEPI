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
       SeedInitialData(modelBuilder);
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
    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        // =======================
        //  Seed COURSES
        // =======================
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "Mathematics 101",
                Description = "Basic algebra and geometry",
                MaximumDegree = 100,
                CreatedAt = new DateTime(2024, 1, 1),
                IsActive = true
            },
            new Course
            {
                Id = 2,
                Title = "Programming Fundamentals",
                Description = "C# and OOP basics",
                MaximumDegree = 100,
                CreatedAt = new DateTime(2024, 1, 1),
                IsActive = true
            },
            new Course
            {
                Id = 3,
                Title = "Physics 101",
                Description = "Mechanics and motion",
                MaximumDegree = 100,
                CreatedAt = new DateTime(2024, 1, 1),
                IsActive = true
            }
        );

        // =======================
        //  Seed STUDENTS
        // =======================
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Name = "Ahmed Hassan",
                Email = "ahmed1@example.com",
                StudentNumber = "STU001",
                EnrollmentDate = new DateTime(2024, 1, 5),
                IsActive = true
            },
            new Student
            {
                Id = 2,
                Name = "Sara Ali",
                Email = "sara@example.com",
                StudentNumber = "STU002",
                EnrollmentDate = new DateTime(2024, 1, 5),
                IsActive = true
            },
            new Student
            {
                Id = 3,
                Name = "Omar Mohamed",
                Email = "omar@example.com",
                StudentNumber = "STU003",
                EnrollmentDate = new DateTime(2024, 1, 5),
                IsActive = true
            },
            new Student
            {
                Id = 4,
                Name = "Mona Fathy",
                Email = "mona@example.com",
                StudentNumber = "STU004",
                EnrollmentDate = new DateTime(2024, 1, 5),
                IsActive = true
            },
            new Student
            {
                Id = 5,
                Name = "Youssef Adel",
                Email = "youssef@example.com",
                StudentNumber = "STU005",
                EnrollmentDate = new DateTime(2024, 1, 5),
                IsActive = true
            }
        );

        // =======================
        //  Seed INSTRUCTORS
        // =======================
        modelBuilder.Entity<Instructor>().HasData(
            new Instructor
            {
                Id = 1,
                Name = "Dr. Mahmoud Salem",
                Email = "mahmoud.salem@example.com",
                Specialization = "Computer Science",
                HireDate = new DateTime(2020, 5, 1),
                IsActive = true
            },
            new Instructor
            {
                Id = 2,
                Name = "Prof. Hanan Fawzy",
                Email = "hanan.fawzy@example.com",
                Specialization = "Mathematics",
                HireDate = new DateTime(2019, 3, 1),
                IsActive = true
            }
        );

        // =======================
        //  Seed EXAMS
        // =======================
        modelBuilder.Entity<Exam>().HasData(
            new Exam
            {
                Id = 1,
                Title = "Math Midterm",
                Description = "Algebra + Geometry",
                TotalMarks = 50,
                Duration = new TimeSpan(1, 0, 0),
                StartDate = new DateTime(2024, 2, 10, 9, 0, 0),
                EndDate = new DateTime(2024, 2, 10, 11, 0, 0),
                CourseId = 1,
                InstructorId = 2,
                IsActive = true
            },
            new Exam
            {
                Id = 2,
                Title = "Programming Quiz",
                Description = "C# Basics",
                TotalMarks = 30,
                Duration = new TimeSpan(0, 30, 0),
                StartDate = new DateTime(2024, 2, 15, 10, 0, 0),
                EndDate = new DateTime(2024, 2, 15, 11, 0, 0),
                CourseId = 2,
                InstructorId = 1,
                IsActive = true
            }
        );

        // =======================
        //  Seed QUESTIONS (TPH)
        // =======================
        modelBuilder.Entity<MulitpleChoiseQuestion>().HasData(
            new
            {
                Id = 1,
                Examid=1,
                QuestionText = "What is 2 + 2?",
                Marks = 5m,
                QuestionType = QuestionType.MultipleChoice,
                CreatedAt = new DateTime(2024, 2, 1),
                ExamId = 1,
                OptionA = "3",
                OptionB = "4",
                OptionC = "5",
                OptionD = "6",
                CorrectAnswer = 'B'
            },
            new
            {
                Id = 2,
                Examid=2,
                QuestionText = "Which keyword defines a class in C#?",
                Marks = 5m,
                QuestionType = QuestionType.MultipleChoice,
                CreatedAt = new DateTime(2024, 2, 1),
                ExamId = 2,
                OptionA = "function",
                OptionB = "class",
                OptionC = "def",
                OptionD = "void",
                CorrectAnswer = 'B'
            }
        );

        modelBuilder.Entity<TrueFalseQuestion>().HasData(
            new
            {
                Id = 3,
                Examid=1,
                QuestionText = "The Earth is flat.",
                Marks = 3m,
                QuestionType = QuestionType.TrueFalse,
                CreatedAt = new DateTime(2024, 2, 1),
                ExamId = 1,
                CorrectAnswer = false
            }
        );

        modelBuilder.Entity<EssayQuestion>().HasData(
            new
            {
                Id = 4,
                Examid = 1,
                QuestionText = "Explain OOP pillars.",
                Marks = 10m,
                QuestionType = QuestionType.Essay,
                CreatedAt = new DateTime(2024, 2, 1),
                ExamId = 2,
                MaxWordCount = 300,
                GradingCriteria = "Clarity, examples, correctness"
            });
            modelBuilder.Entity<StudentCourse>().HasData(
    new StudentCourse
    {
        StudentId = 1,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 1, 10),
        IsCompleted = false,
        Grade = null
    },
    new StudentCourse
    {
        StudentId = 2,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 1, 12),
        IsCompleted = false,
        Grade = null
    },
    new StudentCourse
    {
        StudentId = 1,
        CourseId = 2,
        EnrollmentDate = new DateTime(2024, 1, 15),
        IsCompleted = false,
        Grade = null
    },
    new StudentCourse
    {
        StudentId = 3,
        CourseId = 2,
        EnrollmentDate = new DateTime(2024, 1, 15),
        IsCompleted = false,
        Grade = null
    },
    new StudentCourse
    {
        StudentId = 4,
        CourseId = 2,
        EnrollmentDate = new DateTime(2024, 1, 15),
        IsCompleted = false,
        Grade = null
    },
    new StudentCourse
    {
        StudentId = 5,
        CourseId = 3,
        EnrollmentDate = new DateTime(2024, 1, 20),
        IsCompleted = false,
        Grade = null
    }
        );
        modelBuilder.Entity<InstructorCourse>().HasData(
    new InstructorCourse
    {
        InstructorId = 1,
        CourseId = 2,
        AssignedDate = new DateTime(2024, 1, 5),
        IsActive = true
    },
    new InstructorCourse
    {
        InstructorId = 2,
        CourseId = 1,
        AssignedDate = new DateTime(2024, 1, 5),
        IsActive = true
    },
    new InstructorCourse
    {
        InstructorId = 2,
        CourseId = 3,
        AssignedDate = new DateTime(2024, 1, 5),
        IsActive = true
    }
);
    }
}
