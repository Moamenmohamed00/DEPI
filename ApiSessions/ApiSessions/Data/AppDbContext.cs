using ApiSessions.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiSessions.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("defaultconnection");//in console app
        //}
        public DbSet<TaskItem> tasks { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //fluent api
        //  //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // prefer configuration folder
        //}

    }
}
