using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class InstructorConfiguration:IEntityTypeConfiguration<Instructor>
    {

        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Email)
                .IsRequired();

            builder.HasIndex(i => i.Email).IsUnique();

            builder.Property(i => i.Specialization)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(i => i.HireDate)
                .IsRequired();
        }
    }
}
