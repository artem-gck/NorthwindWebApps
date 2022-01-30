namespace NorthwindMvcClient.ViewModels
{
    public class EmployeesListViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}
