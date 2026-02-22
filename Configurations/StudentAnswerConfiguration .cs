using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
    {
        public void Configure(EntityTypeBuilder<StudentAnswer> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.AnswerText)
                .HasMaxLength(2000);

            builder.Property(sa => sa.MarksObtained)
                .HasColumnType("decimal(10,2)");

            builder.Property(sa => sa.SubmittedAt).IsRequired();

            builder.HasOne(sa => sa.Question)
                .WithMany(q => q.StudentAnswers)
                .HasForeignKey(sa => sa.QuestionId);
        }
    }
}
