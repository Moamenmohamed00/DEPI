using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.TotalMarks)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.Duration)
                .IsRequired();

            builder.Property(e => e.StartDate).IsRequired();
            builder.Property(e => e.EndDate).IsRequired();

            builder.HasCheckConstraint("CK_Exam_EndDate", "EndDate > StartDate");

            builder.HasIndex(e => e.StartDate);

            builder.HasOne(e => e.Instructor)
                   .WithMany(i => i.Exams)
                   .HasForeignKey(e => e.InstructorId);

            builder.HasMany(e => e.Questions)
                   .WithOne(q => q.Exam)
                   .HasForeignKey(q => q.Examid)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
