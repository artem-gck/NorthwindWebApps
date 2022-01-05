using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <summary>
    /// Represents a stub for a product management service.
    /// </summary>
    public sealed class ProductManagementService : IProductManagementService
    {
        private static NorthwindContext? _context;

        public ProductManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task<int> CreateProductAsync(Product product)
        {
            _ = product is null ? throw new ArgumentNullException($"{nameof(product)} is null") : product;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product is null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<ProductCategory>> LookupCategoriesByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> LookupProductsByNameAsync(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<Product>> ShowProductsAsync(int offset, int limit)
            => limit != -1 ? _context.Products.Skip(offset).Take(limit).ToList() : _context.Products.Skip(offset).ToList();

        /// <inheritdoc/>
        public async Task<IList<Product>> ShowProductsForCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            product = _context.Products.Find(productId);

            if (product is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProductAsync(int productId, Product product)
        {
            var prod = _context.Products
                .Where(prod => prod.Id == productId)
                .FirstOrDefault();

            prod.Name = product.Name;   
            prod.SupplierId = product.SupplierId;   
            prod.UnitsOnOrder = product.UnitsOnOrder;
            prod.UnitsInStock = product.UnitsInStock;
            prod.QuantityPerUnit = product.QuantityPerUnit;
            prod.UnitPrice = product.UnitPrice; 
            prod.Discontinued = product.Discontinued;
            prod.CategoryId = product.CategoryId;
            prod.ReorderLevel = product.ReorderLevel;


            await _context.SaveChangesAsync();

            if (_context.Products.Contains(product))
            {
                return true;
            }

            return false;
        }
    }
}
