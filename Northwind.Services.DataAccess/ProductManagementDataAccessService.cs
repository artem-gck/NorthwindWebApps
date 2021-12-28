using Northwind.DataAccess;
using Northwind.DataAccess.Products;
using Northwind.Services.Models;

namespace Northwind.Services.DataAccess
{
    public class ProductManagementDataAccessService : IProductManagementService
    {
        private static NorthwindDataAccessFactory? _factory;

        public ProductManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

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

        public IList<ProductCategory> LookupCategoriesByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

        public IList<Product> LookupProductsByName(IList<string> names)
        {
            throw new NotImplementedException();
        }

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

        public IList<Product> ShowProductsForCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

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