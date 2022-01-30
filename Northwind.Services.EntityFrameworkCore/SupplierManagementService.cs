using Northwind.Services.EntityFrameworkCore.Entities;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.Models;

namespace Northwind.Services.EntityFrameworkCore
{
    public class SupplierManagementService : ISupplierManagementService
    {
        private static NorthwindContext? _context;

        public SupplierManagementService(NorthwindContext context)
            => _context = context;

        public async Task<IList<Models.Supplier>> ShowSupplyersAsync(int offset, int limit)
            => limit != -1 ? _context.Suppliers.Skip(offset).Take(limit).Select(supplier => GetSupplierMod(supplier)).ToList() : _context.Suppliers.Skip(offset).Select(supplier => GetSupplierMod(supplier)).ToList();

        private static Models.Supplier GetSupplierMod(Entities.Supplier supplier)
            => new()
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                PostalCode = supplier.PostalCode,
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
                HomePage = supplier.HomePage,
            };
    }
}
