using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.EntityFrameworkCore.Entities;
using Northwind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.EntityFrameworkCore
{
    /// <inheritdoc/>
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private static NorthwindContext? _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManagementService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeManagementService(NorthwindContext context)
            => _context = context;

        /// <inheritdoc/>
        public async Task<int> CreateEmployeeAsync(Models.Employee employee)
        {
            _ = employee is null ? throw new ArgumentNullException($"{nameof(employee)} is null") : employee;

            await _context.Employees.AddAsync(GetEmloyeeEnt(employee));
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        /// <inheritdoc/>
        public async Task<bool> DestroyEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);

            if (employee is null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<IList<Models.Employee>> ShowEmployeesAsync(int offset, int limit)
            => limit != -1 ? _context.Employees.Skip(offset).Take(limit).Select(employee => GetEmloyeeMod(employee)).ToList() : _context.Employees.Skip(offset).Select(employee => GetEmloyeeMod(employee)).ToList();

        /// <inheritdoc/>
        public bool TryShowEmployee(int employeeId, out Models.Employee employee)
        {
            employee = GetEmloyeeMod(_context.Employees.Find(employeeId));

            if (employee is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateEmployeeAsync(int employeeId, Models.Employee employee)
        {
            var emp = _context.Employees
                .Where(cat => cat.EmployeeId == employeeId)
                .FirstOrDefault();

            emp.EmployeeId = employee.Id;
            emp.LastName = employee.LastName;
            emp.FirstName = employee.FirstName;
            emp.Title = employee.Title;
            emp.TitleOfCourtesy = employee.TitleOfCourtesy;
            emp.BirthDate = employee.BirthDate;
            emp.HireDate = employee.HireDate;
            emp.Address = employee.Address;
            emp.City = employee.City;
            emp.Region = employee.Region;
            emp.PostalCode = employee.PostalCode;
            emp.Country = employee.Country;
            emp.HomePhone = employee.HomePhone;
            emp.Extension = employee.Extension;
            emp.Photo = employee.Photo;
            emp.Notes = employee.Notes;
            emp.ReportsTo = employee.ReportsTo;
            emp.PhotoPath = employee.PhotoPath;

            await _context.SaveChangesAsync();

            if (_context.Employees.Contains(GetEmloyeeEnt(employee)))
            {
                return true;
            }

            return false;
        }

        private static Entities.Employee GetEmloyeeEnt(Models.Employee employee)
            => new()
            {
                EmployeeId = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };

        private static Models.Employee GetEmloyeeMod(Entities.Employee employee)
            => new()
            {
                Id = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
            };
    }
}
