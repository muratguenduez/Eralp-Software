using EralpSoftTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EralpSoftTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {            
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure your entity models here
            //Example:
            modelBuilder.Entity<UserModel>().HasKey(u => u.Id);
            modelBuilder.Entity<ProductModel>().HasKey(p => p.Id);
        }
    }

}
