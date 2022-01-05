using Northwind.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    /// <summary>
    /// IEmployeeManagementService interface.
    /// </summary>
    public interface IEmployeeManagementService
    {
        int CreateEmployee(Employee employee);

        IList<Employee> ShowEmployees(int offset, int limit);

        bool TryShowEmployee(int employeeId, out Employee employee);

        bool DestroyEmployee(int employeeId);

        bool UpdateEmployee(int employeeId, Employee employee);
    }
}
