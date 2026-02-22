using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class ExamAttempConfiguration : IEntityTypeConfiguration<ExamAttempt>
    {
        public void Configure(EntityTypeBuilder<ExamAttempt> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.StartTime).IsRequired();

            builder.Property(a => a.TotalScore)
                .HasColumnType("decimal(10,2)");

            builder.HasIndex(a => a.StartTime);

            builder.HasOne(a => a.Exam)
                .WithMany(e => e.ExamAttempts)
                .HasForeignKey(a => a.ExamId);

            builder.HasMany(a => a.StudentAnswers)
                .WithOne(sa => sa.ExamAttempt)
                .HasForeignKey(sa => sa.ExamAttemptId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
