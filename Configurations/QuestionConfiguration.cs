using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class QuestionConfiguration:IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(q => q.Marks)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.HasCheckConstraint("CK_Question_Marks", "Marks > 0");

            // TPH Configuration
            builder.HasDiscriminator<QuestionType>("QuestionType")
                   .HasValue<MulitpleChoiseQuestion>(QuestionType.MultipleChoice)
                   .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse)
                   .HasValue<EssayQuestion>(QuestionType.Essay);
        }
    }
}
