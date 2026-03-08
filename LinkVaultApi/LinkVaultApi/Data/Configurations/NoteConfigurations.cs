using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVaultApi.Data.Configurations
{
    public class NoteConfigurations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");
            builder.HasKey(n => n.Id);
            builder.Property(n=>n.Title).HasMaxLength(200).IsRequired(true);
            builder.Property(n=>n.Content).HasMaxLength(1000).IsRequired(true);
            builder.Property(n => n.IsPinned).HasDefaultValue(false);
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(n=>n.categoty)
                .WithMany(c=>c.notes)
                .HasForeignKey(n=>n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);//if i delete note category stay//can't delete category has note
        }
    }
}
