using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Job_Requests.Models.Enums;

namespace Job_Requests.Models
{
    public class JobPriority
    {
        public int JobPriorityId { get; set; }

        [Required]
        [DisplayName("Priority Level")]
        public string PriorityLevel { get; set; }

        [DisplayName("Priority Description")]
        public string? PriorityDescription { get; set; }

        public RecordStatusEnum Status { get; set; }

        // Auditing fields
        [DisplayName("Date Created")]
        public DateTime? CreatedDate { get; set; }

		[DisplayName("Date Updated")]
		public DateTime? UpdatedDate { get; set; }

    }
}
