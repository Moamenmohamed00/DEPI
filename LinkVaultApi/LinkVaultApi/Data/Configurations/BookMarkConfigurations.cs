using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkVaultApi.Data.Configurations
{
    public class BookMarkConfigurations : IEntityTypeConfiguration<BookMark>
    {
        public void Configure(EntityTypeBuilder<BookMark> builder)
        {
            builder.ToTable("BookMarks");
            builder.HasKey(bm => bm.Id);
            builder.Property(bm=>bm.URL).HasMaxLength(200).IsRequired();
            builder.HasIndex(bm=>bm.URL).IsUnique();
            builder.Property(bm=>bm.Title).HasMaxLength(100).IsRequired();
            builder.Property(bm=>bm.IsFavorite).HasDefaultValue(false);
            builder.Property(bm=>bm.IsArchived).HasDefaultValue(false);
            builder.Property(bm => bm.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasCheckConstraint("CK_BookMark_URL_Valid", "URL LIKE 'http://%' OR URL LIKE 'https://%' OR URL LIKE 'ftp://%'");

            builder.HasOne(bm => bm.categoty)
                .WithMany(c => c.bookMarks)
                .HasForeignKey(bm => bm.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);//If someone tries to delete a Category that has BookMarks, the operation will be prevented

        }
    }
}
