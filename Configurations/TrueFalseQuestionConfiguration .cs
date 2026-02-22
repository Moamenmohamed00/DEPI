using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    public class TrueFalseQuestionConfiguration : IEntityTypeConfiguration<TrueFalseQuestion>
    {
        public void Configure(EntityTypeBuilder<TrueFalseQuestion> builder)
        {
            builder.Property(q => q.CorrectAnswer).IsRequired();
        }
    }
}
