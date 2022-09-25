using FMS_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace FMS_Store
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions <ProductDBContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}
