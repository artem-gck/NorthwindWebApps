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
        public int CreateEmployee(Employee employee)
        {
            _ = employee is null ? throw new ArgumentNullException($"{nameof(employee)} is null") : employee;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee.Id;
        }

        /// <inheritdoc/>
        public bool DestroyEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);

            if (employee is null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public IList<Employee> ShowEmployees(int offset, int limit)
            => limit != -1 ? _context.Employees.Skip(offset).Take(limit).ToList() : _context.Employees.Skip(offset).ToList();

        /// <inheritdoc/>
        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            employee = _context.Employees.Find(employeeId);

            if (employee is null)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool UpdateEmployee(int employeeId, Employee employee)
        {
            var emp = _context.Employees
                .Where(cat => cat.Id == employeeId)
                .FirstOrDefault();

            emp.Id = employee.Id;
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

            _context.SaveChanges();

            if (_context.Employees.Contains(employee))
            {
                return true;
            }

            return false;
        }
    }
}
