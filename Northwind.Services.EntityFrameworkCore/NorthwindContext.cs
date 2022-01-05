using Microsoft.EntityFrameworkCore;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <summary>
    /// NorthwindContext class.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class NorthwindContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NorthwindContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
           : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<ProductCategory> Categories { get; set; } = null!;

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products { get; set; } = null!;

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public DbSet<Employee> Employees { get; set; } = null!;
    }
}
