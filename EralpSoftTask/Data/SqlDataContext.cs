using EralpSoftTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EralpSoftTask.Data
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions<SqlDataContext> options) : base(options)
        {            
        }

        public DbSet<ProductModel> tblProduct { get; set; }
        public DbSet<UserModel> tblUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasKey(x => x.id);
            modelBuilder.Entity<ProductModel>().Property(b => b.price).HasColumnType("decimal(10,2");

            modelBuilder.Entity<UserModel>().HasKey(x => x.id);
            base.OnModelCreating(modelBuilder);
        }

    }

}
