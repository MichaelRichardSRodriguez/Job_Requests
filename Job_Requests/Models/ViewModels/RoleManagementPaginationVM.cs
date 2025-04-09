namespace Job_Requests.Models.ViewModels
{
	public class RoleManagementPaginationVM
	{
		public IEnumerable<RoleManagementVM> UsersAndRoles { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int RecordCount { get; set; }
		public int TotalUsers { get; set; }

	}
}
