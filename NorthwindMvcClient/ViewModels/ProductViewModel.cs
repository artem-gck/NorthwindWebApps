using System.ComponentModel;

namespace NorthwindMvcClient.ViewModels
{
    public class ProductViewModel
    {
        /// <summary>
        /// Gets or sets a product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a supplier identifier.
        /// </summary>
        [DisplayName("Supplier")]
        public int? SupplierId { get; set; }

        /// <summary>
        /// Gets or sets a category identifier.
        /// </summary>
        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets a quantity per unit.
        /// </summary>
        [DisplayName("Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Gets or sets a unit price.
        /// </summary>
        [DisplayName("Price")]
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets an amount of units in stock.
        /// </summary>
        [DisplayName("Units In Stock")]
        public short? UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets an amount of units on order.
        /// </summary>
        [DisplayName("Units On Order")]
        public short? UnitsOnOrder { get; set; }

        /// <summary>
        /// Gets or sets a reorder level.
        /// </summary>
        [DisplayName("Reorder Level")]
        public short? ReorderLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a product is discontinued.
        /// </summary>
        public bool Discontinued { get; set; }
    }
}
