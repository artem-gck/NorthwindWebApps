using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    public class ProductCategoriesManagementDataAccessService : IProductCategoryManagementService
    {
        private static NorthwindDataAccessFactory? _factory;

        public ProductCategoriesManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        public int CreateCategory(ProductCategory productCategory)
        {
            _ = productCategory is null ? throw new ArgumentNullException($"{nameof(productCategory)} is null") : productCategory;

            var category = new ProductCategoryTransferObject
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return _factory.GetProductCategoryDataAccessObject().InsertProductCategory(category);
        }

        public bool DestroyCategory(int categoryId)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = access.FindProductCategory(categoryId);

                if (category is null)
                {
                    return false;
                }

                access.DeleteProductCategory(categoryId);

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }
        }

        public IList<ProductCategory> ShowCategories(int offset, int limit)
            => _factory.GetProductCategoryDataAccessObject().SelectProductCategories(offset, limit).Select(category =>
            {
                return new ProductCategory
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Picture = category.Picture,
                };
            }).ToList();

        public bool TryShowCategory(int categoryId, out ProductCategory productCategory)
        {
            try
            {
                var category = _factory.GetProductCategoryDataAccessObject().FindProductCategory(categoryId);

                if (category is null)
                {
                    productCategory = null;

                    return false;
                }

                productCategory = new ProductCategory
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Picture = category.Picture,
                };

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                productCategory = null;
                return false;
            }
        }

        public bool UpdateCategories(int categoryId, ProductCategory productCategory)
        {
            var category = new ProductCategoryTransferObject
            {
                Name = productCategory.Name,
                Description = productCategory.Description,
                Picture = productCategory.Picture,
            };

            return _factory.GetProductCategoryDataAccessObject().UpdateProductCategory(category);
        }
    }
}
