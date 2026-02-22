using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Email)
            .IsRequired();

        builder.HasIndex(s => s.Email)
               .IsUnique();

        builder.Property(s => s.StudentNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(s => s.StudentNumber).IsUnique();

        builder.Property(s => s.EnrollmentDate)
            .IsRequired();

        builder.HasMany(s => s.ExamAttempts)
               .WithOne(a => a.Student)
               .HasForeignKey(a => a.StudentId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
