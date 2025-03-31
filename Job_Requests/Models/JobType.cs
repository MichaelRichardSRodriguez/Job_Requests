using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Job_Requests.Models
{
    public class JobType
    {
        public int JobTypeId { get; set; }

		[Required(ErrorMessage = "Job Type is required.")]
		[DisplayName("Job Type")]
        [StringLength(20, ErrorMessage = "Job Type cannot be longer than 20 characters.")]
        public string JobTypeName { get; set; }

        [DisplayName("Description")]
		[Required(ErrorMessage = "Description is required.")]
		[StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string? JobTypeDescription { get; set; }

        public RecordStatusEnum Status { get; set; }

		// Auditing fields
		[DisplayName("Date Created")]
		public DateTime? CreatedDate { get; set; }
		[DisplayName("Date Updated")]
		public DateTime? UpdatedDate { get; set; }

        [ValidateNever]
        public IEnumerable<JobRequest> JobRequests { get; set; }

    }
}
