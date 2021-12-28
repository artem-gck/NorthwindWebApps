using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    public class ProductCategoryPicturesManagementDataAccessService : IProductCategoryPicturesService
    {
        private static NorthwindDataAccessFactory? _factory;

        public ProductCategoryPicturesManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

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
