using Microsoft.EntityFrameworkCore;

namespace Store.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductsDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}