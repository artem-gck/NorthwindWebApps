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
        /// <summary>
        /// Creates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>An identifier of a created employee.</returns>
        int CreateEmployee(Employee employee);

        /// <summary>
        /// Shows the employees.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="Employee"/>.</returns>
        IList<Employee> ShowEmployees(int offset, int limit);

        /// <summary>
        /// Tries the show employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>Returns true if a employee is returned; otherwise false.</returns>
        bool TryShowEmployee(int employeeId, out Employee employee);

        /// <summary>
        /// Destroys the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>True if a employee is destroyed; otherwise false.</returns>
        bool DestroyEmployee(int employeeId);

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>True if a employee is updated; otherwise false.</returns>
        bool UpdateEmployee(int employeeId, Employee employee);
    }
}
