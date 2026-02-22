using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    public class EssayQuestionConfiguration : IEntityTypeConfiguration<EssayQuestion>
    {
        public void Configure(EntityTypeBuilder<EssayQuestion> builder)
        {
            builder.Property(e => e.GradingCriteria)
                .HasMaxLength(1000);
        }
    }
}
