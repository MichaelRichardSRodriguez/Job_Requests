namespace Job_Requests.Models.ViewModels
{
	public class DepartmentPaginationVM
	{

		public List<Department> Departments { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int RecordCount { get; set; }
		public int TotalDepartments { get; set; }

	}
}
