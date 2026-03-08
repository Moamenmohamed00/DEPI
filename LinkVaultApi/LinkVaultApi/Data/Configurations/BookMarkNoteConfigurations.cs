using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVaultApi.Data.Configurations
{
    public class BookMarkNoteConfigurations : IEntityTypeConfiguration<BookMarkNotes>
    {
        public void Configure(EntityTypeBuilder<BookMarkNotes> builder)
        {
            builder.ToTable("BookMarkNotes");
            builder.HasKey(bmn => bmn.Id);
            builder.Property(bmn => bmn.Content).HasMaxLength(200).IsRequired(true);
            builder.Property(bmn => bmn.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(bmn=>bmn.BookMark)
                .WithMany(bm=>bm.Notes)
                .HasForeignKey(bmn=>bmn.BookMarkId)
                .OnDelete(DeleteBehavior.Cascade);//if i delete bookmark will delete bmNotes that contains//if i delete BookMarkNotes book mark stay
        }
    }
}
