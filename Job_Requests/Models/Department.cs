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
		public string DepartmentName { get; set; }
        [DisplayName("Description")]
        public string DepartmentDescription { get; set; }
		public RecordStatusEnum Status { get; set; }

		[ValidateNever]
		public IEnumerable<JobRequest> JobRequests { get; set; }
	}
}
