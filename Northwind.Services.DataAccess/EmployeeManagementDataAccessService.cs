using Northwind.DataAccess;
using Northwind.DataAccess.Employees;
using Northwind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.DataAccess
{
    public class EmployeeManagementDataAccessService : IEmployeeManagementService
    {
        private static NorthwindDataAccessFactory? _factory;

        public EmployeeManagementDataAccessService(NorthwindDataAccessFactory factory)
            => _factory = factory;

        public int CreateEmployee(Employee employee)
        {
            _ = employee is null ? throw new ArgumentNullException($"{nameof(employee)} is null") : employee;

            var emp = GetEmployeeTransferObject(employee);

            return _factory.GetEmployeeDataAccessObject().InsertEmployee(emp);
        }

        public bool DestroyEmployee(int employeeId)
        {
            var access = _factory.GetEmployeeDataAccessObject();

            try
            {
                var emplouee = access.FindEmployee(employeeId);

                if (emplouee is null)
                {
                    return false;
                }

                access.DeleteEmployee(employeeId);

                return true;
            }
            catch (EmployeeNotFoundException)
            {
                return false;
            }
        }

        public IList<Employee> ShowEmployees(int offset, int limit)
            => _factory.GetEmployeeDataAccessObject().SelectEmployee(offset, limit).Select(employee =>
            {
                return GetEmloyee(employee);
            }).ToList();

        public bool TryShowEmployee(int employeeId, out Employee employee)
        {
            try
            {
                var emp = _factory.GetEmployeeDataAccessObject().FindEmployee(employeeId);

                if (emp is null)
                {
                    employee = null;

                    return false;
                }

                employee = GetEmloyee(emp);

                return true;
            }
            catch (EmployeeNotFoundException)
            {
                employee = null;
                return false;
            }
        }

        public bool UpdateEmployee(int employeeId, Employee employee)
        {
            var emp = GetEmployeeTransferObject(employee);

            return _factory.GetEmployeeDataAccessObject().UpdateEmployee(emp);
        }

        private static EmployeeTransferObject GetEmployeeTransferObject(Employee employee)
            => new EmployeeTransferObject
            {
                Id = employee.Id,
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

        private static Employee GetEmloyee(EmployeeTransferObject employee)
            => new Employee
            {
                Id = employee.Id,
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
