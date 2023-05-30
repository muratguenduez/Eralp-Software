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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductModel>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.userid);
        }

    }

}
