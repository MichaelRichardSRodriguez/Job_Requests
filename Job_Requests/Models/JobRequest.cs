using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Requests.Models
{
    public class JobRequest
    {

		[DisplayName("JR#")]
		public int JobRequestId { get; set; }
        [DisplayName("Requesting Department")]
		[Required(ErrorMessage = "Requesting Department is required.")]
		public int RequestingDepartmentId { get; set; }
		[DisplayName("Receiving Department")]
		[Required(ErrorMessage = "Receiving Department is required.")]
		public int ReceivingDepartmentId { get; set; }
		[DisplayName("Type")]
		[Required(ErrorMessage = "Job Type is required.")]
		public int JobTypeId { get; set; }
		[DisplayName("Priority Level")]
		public int? JobPriorityId { get; set; }

        // public int EmployeeId { get; set; }

        [DisplayName("Description")]
		[Required(ErrorMessage = "Description is required.")]
		public string JobDescription { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm tt}")]
		[DisplayName("Date Requested")]
        public DateTime? RequestDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm tt}")]
		[DisplayName("Date Completed")]
        public DateTime? DateCompleted { get; set; }

        // Enum for Status
        public JobStatusEnum Status { get; set; }

        [StringLength(200, ErrorMessage = "Remarks cannot be longer than 200 characters.")]
        public string? Remarks { get; set; }


        // Navigational Properties
        [ForeignKey("RequestingDepartmentId")]
		[DisplayName("Requestor")]
		[ValidateNever]
        public Department RequestingDepartment { get; set; }

        [ForeignKey("ReceivingDepartmentId")]
		[DisplayName("Receiver")]
		[ValidateNever]
        public Department ReceivingDepartment { get; set; }

        [ForeignKey("JobTypeId")]
		[DisplayName("Type")]
		[ValidateNever]
        public JobType JobType { get; set; }

        [ForeignKey("JobPriorityId")]
        [ValidateNever]
        public JobPriority? JobPriority { get; set; }


        //public Employee Employee { get; set; }
    }
}
