using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Job_Requests.Models
{
	public class Department
	{
		public int DepartmentId { get; set; }
		[DisplayName("Name")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string DepartmentName { get; set; }
        [DisplayName("Description")]
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string DepartmentDescription { get; set; }
		public RecordStatusEnum Status { get; set; }

		[ValidateNever]
		public IEnumerable<JobRequest> JobRequestsAsRequestingDepartment { get; set; }
		public IEnumerable<JobRequest> JobRequestsAsReceivingDepartment { get; set; }
	}
}
