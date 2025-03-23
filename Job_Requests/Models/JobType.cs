using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Job_Requests.Models.Enums;

namespace Job_Requests.Models
{
    public class JobType
    {
        public int JobTypeId { get; set; }

        [Required]
        [DisplayName("Job Type")]
        [StringLength(20, ErrorMessage = "Job Type cannot be longer than 20 characters.")]
        public string JobTypeName { get; set; }

        [DisplayName("Description")]
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters.")]
        public string? JobTypeDescription { get; set; }

        public RecordStatusEnum Status { get; set; }

		// Auditing fields
		[DisplayName("Date Created")]
		public DateTime? CreatedDate { get; set; }
		[DisplayName("Date Updated")]
		public DateTime? UpdatedDate { get; set; }

    }
}
