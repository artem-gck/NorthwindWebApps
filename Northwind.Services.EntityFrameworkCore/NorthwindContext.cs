using Microsoft.EntityFrameworkCore;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
           : base(options)
        {
        }

        public DbSet<ProductCategory> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Picture> Pictures { get; set; } = null!;
    }
}
