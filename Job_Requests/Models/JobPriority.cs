﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Job_Requests.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Job_Requests.Models
{
    public class JobPriority
    {
        public int JobPriorityId { get; set; }

		[Required(ErrorMessage = "Priority Level is required.")]
		[DisplayName("Priority Level")]
        [StringLength(10, ErrorMessage = "Job Priority cannot be longer than 10 characters.")]
        public string PriorityLevel { get; set; }

        [DisplayName("Priority Description")]
		[Required(ErrorMessage = "Description is required.")]
		[StringLength(300, ErrorMessage = "Job Priority cannot be longer than 300 characters.")]
        public string PriorityDescription { get; set; }

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
