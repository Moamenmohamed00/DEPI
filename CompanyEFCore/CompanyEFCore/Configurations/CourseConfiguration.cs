using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace CompanyEFCore.Configurations
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x=>x.Description).HasMaxLength(1000);
            builder.Property(x => x.MaximumDegree).HasColumnType("decimal(3,2)").IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.HasCheckConstraint("CK_Course_MaxDegree","MaximumDegree > 0");
            // Relationships
            builder.HasMany(c => c.Exams)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
