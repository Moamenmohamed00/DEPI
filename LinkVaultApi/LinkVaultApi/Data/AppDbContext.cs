using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVaultApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        public DbSet<Categoty> categoties { get; set; }
        public DbSet<BookMark> bookMarks { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<BookMarkNotes> bookMarksNotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //modelBuilder.Entity<BookMark>()
            //    .HasKey(x => x.Id);
            //base.OnModelCreating(modelBuilder);
        }

    }
}
