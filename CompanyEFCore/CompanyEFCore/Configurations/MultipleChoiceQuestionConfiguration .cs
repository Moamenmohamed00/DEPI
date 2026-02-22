using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class MultipleChoiceQuestionConfiguration:IEntityTypeConfiguration<MulitpleChoiseQuestion>
    {
        public void Configure(EntityTypeBuilder<MulitpleChoiseQuestion> builder)
        {
            builder.Property(p => p.OptionA).IsRequired().HasMaxLength(500);
            builder.Property(p => p.OptionB).IsRequired().HasMaxLength(500);
            builder.Property(p => p.OptionC).IsRequired().HasMaxLength(500);
            builder.Property(p => p.OptionD).IsRequired().HasMaxLength(500);

            builder.Property(p => p.CorrectAnswer)
                .IsRequired()
                .HasMaxLength(1);
        }
    }
}
