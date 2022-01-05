using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    /// <summary>
    /// ProductManagementDataAccessService class.
    /// </summary>
    /// <seealso cref="Northwind.Services.IProductManagementService" />
    public class ProductManagementDataAccessService : IProductManagementService
    {
        private static NorthwindDataAccessFactory? _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementDataAccessService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ProductManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        /// <inheritdoc/>
        public int CreateProduct(Product product)
        {
            _ = product is null ? throw new ArgumentNullException($"{nameof(product)} is null") : product;

            var pro = new ProductTransferObject
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return _factory.GetProductDataAccessObject().InsertProduct(pro);
        }

        /// <inheritdoc/>
        public bool DestroyProduct(int productId)
        {
            var access = _factory.GetProductDataAccessObject();

            try
            {
                var product = access.FindProduct(productId);

                access.DeleteProduct(productId);

                return true;
            }
            catch (ProductNotFoundException)
            {
                return false;
            }
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
            => _factory.GetProductDataAccessObject().SelectProducts(offset, limit).Select(product =>
            {
                return new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    UnitsOnOrder = product.UnitsOnOrder,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                };
            }).ToList();

        /// <inheritdoc/>
        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int productId, out Product product)
        {
            try
            {
                var prod = _factory.GetProductDataAccessObject().FindProduct(productId);

                if (prod is null)
                {
                    product = null;

                    return false;
                }

                product = new Product
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    SupplierId = prod.SupplierId,
                    CategoryId = prod.CategoryId,
                    QuantityPerUnit = prod.QuantityPerUnit,
                    UnitPrice = prod.UnitPrice,
                    UnitsInStock = prod.UnitsInStock,
                    UnitsOnOrder = prod.UnitsOnOrder,
                    ReorderLevel = prod.ReorderLevel,
                    Discontinued = prod.Discontinued,
                };

                return true;
            }
            catch (ProductNotFoundException)
            {
                product = null;
                return false;
            }
        }

        /// <inheritdoc/>
        public bool UpdateProduct(int productId, Product product)
        {
            var pro = new ProductTransferObject
            {
                Name = product.Name,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued,
            };

            return _factory.GetProductDataAccessObject().UpdateProduct(pro);
        }
    }
}