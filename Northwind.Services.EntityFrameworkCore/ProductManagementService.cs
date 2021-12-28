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
        public int CreateProduct(Product product)
        {
            _ = product is null ? throw new ArgumentNullException($"{nameof(product)} is null") : product;

            _context.Products.Add(product);
            _context.SaveChanges();

            return product.Id;
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product is null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IList<Product> ShowProducts(int offset, int limit)
            => limit != -1 ? _context.Products.Skip(offset).Take(limit).ToList() : _context.Products.Skip(offset).ToList();

        /// <inheritdoc/>
        public IList<Product> ShowProductsForCategory(int categoryId)
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
        public bool UpdateProduct(int productId, Product product)
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


            _context.SaveChanges();

            if (_context.Products.Contains(product))
            {
                return true;
            }

            return false;
        }
    }
}
