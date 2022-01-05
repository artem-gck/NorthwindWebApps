

using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryPicturesManagementService : IProductCategoryPicturesService
    {
        private static NorthwindContext? _context;

        public ProductCategoryPicturesManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public bool DestroyPicture(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            category.Picture = null;
            //_context.Categories.Remove(category);
            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var category = _context.Categories.Find(categoryId);

            if (category is null)
            {
                bytes = null;
                return false;
            }

            bytes = category.Picture;
            return true;
        }

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            var category = _context.Categories.Find(categoryId);

            if (category is null)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    category.Picture = memoryStream.ToArray();

                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
