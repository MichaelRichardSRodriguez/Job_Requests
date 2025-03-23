using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Requests.Models
{
    public class JobRequest
    {

        public int JobRequestId { get; set; }
        [DisplayName("Requesting Department")]
        public int DepartmentId { get; set; }

        //public int JobTypeId { get; set; }
        //public int JobPriorityId { get; set; }

        // public int EmployeeId { get; set; }

        [DisplayName("Description")]
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
        [ForeignKey("DepartmentId")]
		[ValidateNever]
        public Department Department { get; set; }

        //[ForeignKey("JobTypeId")]
        //[ValidateNever]
        //public JobType JobType { get; set; }

        //[ForeignKey("JobPriorityId")]
        //[ValidateNever]
        //public JobPriority JobPriority { get; set; }

        //public Employee Employee { get; set; }
    }
}
