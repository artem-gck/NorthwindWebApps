namespace Northwind.DataAccess.Employees
{
    /// <summary>
    /// Represents a DAO for Northwind employees.
    /// </summary>
#pragma warning disable CA1040
    public interface IEmployeeDataAccessObject
#pragma warning restore CA1040
    {
        /// <summary>
        /// Inserts the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>Id of emloyee.</returns>
        int InsertEmployee(EmployeeTransferObject employee);

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>True if a employee is deleted; otherwise false.</returns>
        bool DeleteEmployee(int employeeId);

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>True if a employee is updated; otherwise false.</returns>
        bool UpdateEmployee(EmployeeTransferObject employee);

        /// <summary>
        /// Finds the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>A <see cref="EmployeeTransferObject"/> with specified identifier.</returns>
        EmployeeTransferObject FindEmployee(int employeeId);

        /// <summary>
        /// Selects the employee.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="EmployeeTransferObject"/>.</returns>
        IList<EmployeeTransferObject> SelectEmployee(int offset, int limit);

        /// <summary>
        /// Selects the name of the employee by.
        /// </summary>
        /// <param name="employeeNames">The employee names.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="EmployeeTransferObject"/>.</returns>
        IList<EmployeeTransferObject> SelectEmployeeByName(ICollection<string> employeeNames);
    }
}