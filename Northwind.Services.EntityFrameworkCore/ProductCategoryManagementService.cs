using Northwind.Services;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryManagementService : IProductCategoryManagementService
    {
        private static NorthwindContext? _context;

        public ProductCategoryManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public int CreateCategory(ProductCategory productCategory)
        {
            _ = productCategory is null ? throw new ArgumentNullException($"{nameof(productCategory)} is null") : productCategory;

            _context.Categories.Add(productCategory);
            _context.SaveChanges();

            return productCategory.Id;
        }

        /// <inheritdoc/>
        public bool DestroyCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public IList<ProductCategory> ShowCategories(int offset, int limit)
            => limit != -1 ? _context.Categories.Skip(offset).Take(limit).ToList() : _context.Categories.Skip(offset).ToList();

        /// <inheritdoc/>
        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            productCategory = _context.Categories.Find(categoryId);

            if (productCategory is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            var category = _context.Categories
                .Where(cat => cat.Id == categoryId)
                .FirstOrDefault();

            category.Name = productCategory.Name;
            category.Description = productCategory.Description;

            _context.SaveChanges();

            if (_context.Categories.Contains(productCategory))
            {
                return true;
            }

            return false;
        }
    }
}
