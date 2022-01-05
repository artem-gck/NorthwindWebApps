using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// ProductCategoryPicturesManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IProductCategoryPicturesService" />
    public class ProductCategoryPicturesManagementDataAccessService : IProductCategoryPicturesService
    {
        private static NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryPicturesManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ProductCategoryPicturesManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public bool DestroyPicture(int categoryId)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            var category = access.FindProductCategory(categoryId);

            if (category is null)
            {
                return false;
            }

            category.Picture = null;

            access.UpdateProductCategory(category);

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = access.FindProductCategory(categoryId);
                bytes = category.Picture;
                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                bytes = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            var access = _factory.GetProductCategoryDataAccessObject();

            try
            {
                var category = access.FindProductCategory(categoryId);

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        category.Picture = memoryStream.ToArray();
                        access.UpdateProductCategory(category);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (ProductCategoryNotFoundException)
            {
                return false;
            }
        }
    }
}
