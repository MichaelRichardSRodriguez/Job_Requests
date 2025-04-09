using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Job_Requests.Models.ViewModels
{
	public class RoleManagementVM
	{

		public ApplicationUser ApplicationUser { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> RoleList { get; set; }

		[ValidateNever]
		public IEnumerable<SelectListItem> DepartmentList { get; set; }

	}
}
