using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Requests.Models
{
	public class Department
	{
		public int DepartmentId { get; set; }
		[DisplayName("Name")]
		[Required(ErrorMessage ="Department Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string DepartmentName { get; set; }
        [DisplayName("Description")]
		[Required(ErrorMessage = "Description is required.")]
		[StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string DepartmentDescription { get; set; }
		[DisplayName("Created By")]
		public string? CreatedUserId { get; set; }
		[DisplayName("Updated By")]
		public string? UpdatedUserId { get; set; }
		[DisplayName("Date Created")]
		public DateTime? CreatedDate { get; set; }
		[DisplayName("Date Updated")]
		public DateTime? UpdatedDate { get; set; }

		public RecordStatusEnum Status { get; set; }

		[ValidateNever]
		public IEnumerable<JobRequest> JobRequestsAsRequestingDepartment { get; set; }

		[ValidateNever]
		public IEnumerable<JobRequest> JobRequestsAsReceivingDepartment { get; set; }

        [ValidateNever]
        public IEnumerable<Department> ApplicationUserDepartment { get; set; }

		[ForeignKey("CreatedUserId")]
		[ValidateNever]
		public ApplicationUser DepartmentAsCreatedByUser { get; set; }

        [ForeignKey("UpdatedUserId")]
        [ValidateNever]
		public ApplicationUser DepartmentAsUpdatedByUser { get; set; }
	}
}
