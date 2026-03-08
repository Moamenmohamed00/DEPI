using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVaultApi.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Categoty>
    {
        public void Configure(EntityTypeBuilder<Categoty> builder)
        {
            builder.ToTable("Categories");
           builder.HasKey(c=>c.Id);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c=>c.Description).HasMaxLength(100).IsRequired(false);
            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
