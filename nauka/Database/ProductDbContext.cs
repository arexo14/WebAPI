using Microsoft.EntityFrameworkCore;
using nauka.Entities;

namespace nauka.Database
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product>? Products { get; set; }
    }
}
