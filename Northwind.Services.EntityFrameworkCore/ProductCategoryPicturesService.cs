

using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class ProductCategoryPicturesService : IProductCategoryPicturesService
    {
        private static NorthwindContext? _context;

        public ProductCategoryPicturesService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public bool DestroyPicture(int categoryId)
        {
            var picture = _context.Pictures.Find(categoryId);

            if (picture is null)
            {
                return false;
            }

            _context.Pictures.Remove(picture);
            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowPicture(int categoryId, out byte[] bytes)
        {
            var picture = _context.Pictures.Find(categoryId);

            if (picture is null)
            {
                bytes = null;
                return false;
            }

            bytes = picture.Content;
            return true;
        }

        /// <inheritdoc/>
        public bool UpdatePicture(int categoryId, Stream stream)
        {
            if (_context.Categories.Find(categoryId) is null)
            {
                return false;
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    var file = new Picture()
                    {
                        CategoryId = categoryId,
                        Content = memoryStream.ToArray()
                    };

                    _context.Pictures.Add(file);
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
